import Hero from "../Components/Hero/Hero";
import HowItWorks from "../Components/HowItWorks/HowItWorks";
import Navbar from "../Components/Navbar/Navbar";

export default function Home() {
  return (
    <div className="items-center justify-center h-auto flex-col flex  overflow-x-hidden bg-linear-to-b from-sky-900  to-white w-full">
      <Navbar/>
      <main className="items-center justify-center flex-col flex h-auto overflow-x-hidden">
        <Hero />
        <HowItWorks />
      </main>
    </div>
  );
}
