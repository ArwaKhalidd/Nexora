import { GoogleGenAI } from "@google/genai";

const apiKey = import.meta.env.VITE_GEMINI_API;

const ai = new GoogleGenAI({ apiKey: apiKey });

export async function AIResult(UserMessage) {
  try {
    return await ai.models.generateContentStream({
      model: "gemini-3.1-flash-lite",
      contents: UserMessage,
    });
  } catch (error) {
    console.log("error : ", error);
  }
}
