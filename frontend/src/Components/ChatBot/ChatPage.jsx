import { useEffect, useState } from "react";
import { MdOutlineSend } from "react-icons/md";
import { AIResult } from "../../Services/geminiFetch";
import ReactMarkdown from "react-markdown";
import { Prism as SyntaxHighlighter } from "react-syntax-highlighter";
import { oneDark } from "react-syntax-highlighter/dist/esm/styles/prism";

export default function ChatPage() {
  const [content, setContent] = useState("");
  const [messages, setMessage] = useState(() => {
    const savedMessages = localStorage.getItem("chat_messages");
    return savedMessages ? JSON.parse(savedMessages) : [];
  });
  useEffect(() => {
    localStorage.setItem("chat_messages", JSON.stringify(messages));
  }, [messages]);
  const clearchat = () => {
    localStorage.removeItem("chat_messages");
    setMessage([]);
  };
  const handleSubmit = async () => {
    if (!content.trim()) return;

    const prompt = content;
    setContent("");

    setMessage((prev) => [
      ...prev,
      {
        role: "user",
        text: prompt,
      },
      {
        role: "assistant",
        text: "",
        streaming: true,
      },
    ]);

    try {
      const history = messages
        .filter((msg) => msg.text) //we use filter to filter out messages and the map to make the text in an array that we called it parts and every time it will return the parts as a key and value to make it easy for model to memorize and also we will switch from "assistant" to "model" to make the model able to understand it
        .map((msg) => ({
          role: msg.role === "assistant" ? "model" : "user",
          parts: [{ text: msg.text }],
        }));

      history.push({
        role: "user",
        parts: [{ text: prompt }],
      });

      const result = await AIResult(history);

      for await (const chunk of result) {
        setMessage((prev) => {
          const updated = [...prev];

          updated[updated.length - 1] = {
            ...updated[updated.length - 1],
            text: updated[updated.length - 1].text + (chunk.text || ""),
          };

          return updated;
        });
      }

      setMessage((prev) => {
        const updated = [...prev];

        updated[updated.length - 1] = {
          ...updated[updated.length - 1],
          streaming: false,
        };

        return updated;
      });
    } catch (error) {
      console.error(error);

      setMessage((prev) => {
        const updated = [...prev];

        updated[updated.length - 1] = {
          role: "assistant",
          text: "Something went wrong. Please try again.",
          streaming: false,
        };

        return updated;
      });
    }
  };

  return (
    <div className=" text-black flex flex-col mt-5 h-[75vh] w-full p-2 items-center gap-5 bg-white/40 shadow-2xl shadow-sky-900/10 backdrop-blur z-20 rounded-4xl ">
      <div
        className="w-full 
      bg-transparent overflow-y-auto mb-20 p-6 flex-1 text-gray-400"
      >
        {messages.map((msg, index) => (
          <div
            key={index}
            className={`flex mb-4
            ${msg.role === "user" ? "justify-end" : "justify-start"}`}
          >
            <div
              className={` rounded-lg  ${
                msg.role === "user"
                  ? "bg-black text-xs md:text-lg shadow-xs shadow-gray-600 text-white p-3"
                  : "bg-sky-950/80 text-xs md:text-lg text-white p-5 w-full"
              }`}
            >
              <ReactMarkdown
                components={{
                  a: ({ children, href }) => (
                    <a
                      href={href}
                      target="_blank"
                      rel="noopener noreferrer"
                      className="text-sky-600 underline hover:text-sky-300 transition-colors"
                    >
                      {children}
                    </a>
                  ),
                  h1: ({ children }) => (
                    <h1 className="text-3xl font-bold mb-4">{children}</h1>
                  ),
                  h2: ({ children }) => (
                    <h2 className="text-2xl font-semibold mt-6 mb-3">
                      {children}
                    </h2>
                  ),
                  h3: ({ children }) => (
                    <h3 className="text-xl font-semibold mt-4 mb-2">
                      {children}
                    </h3>
                  ),
                  p: ({ children }) => (
                    <p className="mb-3 leading-7 text-white font-bold font-serif">
                      {children}
                    </p>
                  ),
                  ul: ({ children }) => (
                    <ul className="list-disc pl-6 mb-3">{children}</ul>
                  ),
                  ol: ({ children }) => (
                    <ol className="list-decimal pl-6 mb-3">{children}</ol>
                  ),
                  li: ({ children }) => <li className="mb-1">{children}</li>,
                  code({ inline, className, children }) {
                    const match = /language-(\w+)/.exec(className || "");

                    return !inline && match ? (
                      <SyntaxHighlighter
                        style={oneDark}
                        language={match[1]}
                        PreTag="div"
                      >
                        {String(children).replace(/\n$/, "")}
                      </SyntaxHighlighter>
                    ) : (
                      <code className="text-gray-400 px-1 py-0.5 rounded">
                        {children}
                      </code>
                    );
                  },
                  pre: ({ children }) => (
                    <pre className="bg-gray-900 text-white p-4 rounded-lg overflow-x-auto my-4">
                      {children}
                    </pre>
                  ),
                }}
              >
                {msg.text}
              </ReactMarkdown>
            </div>
          </div>
        ))}
      </div>
      <div
        className="flex items-center mb-3
    justify-between w-[90%] fixed bottom-0 "
      >
        <button
          onClick={clearchat}
          className="bg-red-700 text-sky-50 rounded-md font-serif px-3 py-3 font-bold cursor-pointer duration-700 hover:bg-red-400 w-[15%]"
        >
          New Chat
        </button>
        <div className="flex items-center font-bold justify-between rounded-md font-serif px-3 py-1.5 w-[80%] bg-sky-900 ">
          <input
            value={content}
            onChange={(text) => setContent(text.target.value)}
            placeholder="Ask Nexora Assistant"
            onKeyDown={(inputKey) => {
              if (inputKey.key === "Enter") {
                inputKey.preventDefault();
                handleSubmit();
              }
            }}
            type="text"
            className="w-[90%] text-start text-xl bg-transparent border-0 outline-0 text-sky-100 p-1 "
          />
          <MdOutlineSend
            onClick={handleSubmit}
            className="text-white  font-bold text-xl cursor-pointer duration-700 ease-in-out hover:text-sky-200"
          />
        </div>
      </div>
    </div>
  );
}
