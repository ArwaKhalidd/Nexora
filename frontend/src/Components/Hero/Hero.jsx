import { useNavigate } from "react-router-dom";
import { SlLogin } from "react-icons/sl";
import { FaArrowRight, FaArrowDown, FaArrowTrendUp } from "react-icons/fa6";
import { useEffect, useState } from "react";
import Loader from "../Loader/Loader";

const STATS = [
  ["10K+", "Students"],
  ["120+", "Universities"],
  ["98%", "Success rate"],
];

const METRICS = [
  { label: "Engagement", value: 92 },
  { label: "Attendance", value: 88 },
  { label: "Focus Score", value: 95 },
];

const CHART_PATH =
  "M4,110 C40,104 55,92 78,86 C104,79 118,60 148,58 C176,56 188,40 214,32 C240,24 258,20 296,10";

export default function Hero() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [mounted, setMounted] = useState(false);

  useEffect(() => {
    document.body.style.overflow = loading ? "hidden" : "auto";

    return () => {
      document.body.style.overflow = "auto";
    };
  }, [loading]);

  useEffect(() => {
    const t = setTimeout(() => setMounted(true), 150);
    return () => clearTimeout(t);
  }, []);

  const navigateSignupPage = () => {
    setLoading(true);

    setTimeout(() => {
      navigate("/signup");
    }, 2000);
  };

  const NavigateSigninPage = () => {
    setLoading(true);
    setTimeout(() => {
      navigate("/signin");
    }, 2000);
  };

  return (
    <>
      {loading && <Loader />}

      <style>{`
        @keyframes draw-trend {
          from { stroke-dashoffset: 420; }
          to { stroke-dashoffset: 0; }
        }
        .draw-trend {
          stroke-dasharray: 420;
          animation: draw-trend 1.6s ease-out forwards;
        }
        @keyframes fade-up {
          from { opacity: 0; transform: translateY(10px); }
          to { opacity: 1; transform: translateY(0); }
        }
        .fade-up {
          opacity: 0;
          animation: fade-up 0.7s ease-out forwards;
        }
      `}</style>

      <section
        id="home"
        className="relative scroll-mt-28 flex min-h-[calc(100vh-150px)] w-full items-center justify-center py-12 md:py-16"
      >
        <div className="relative z-10 grid w-full max-w-6xl grid-cols-1 items-center gap-14 lg:grid-cols-2 lg:gap-10">
          <div className="flex flex-col items-center text-center lg:items-start lg:text-left">
            <span
              className="fade-up inline-flex items-center gap-2 rounded-full border border-sky-200 bg-white/70 px-4 py-1.5 text-xs font-bold uppercase tracking-wider text-sky-700 shadow-sm backdrop-blur"
              style={{ animationDelay: "0ms" }}
            >
              <span className="relative flex h-2 w-2">
                <span className="absolute inline-flex h-full w-full animate-ping rounded-full bg-sky-400 opacity-75" />
                <span className="relative inline-flex h-2 w-2 rounded-full bg-sky-600" />
              </span>
              Real-Time Academic Intelligence
            </span>

            <h1
              className="fade-up mt-5 max-w-2xl text-4xl font-black leading-tight text-sky-950 sm:text-5xl lg:text-6xl"
              style={{ animationDelay: "80ms" }}
            >
              Transform Learning into Measurable Success
            </h1>

            <p
              className="fade-up mt-6 max-w-xl text-base font-medium leading-8 text-slate-medium sm:text-lg"
              style={{ animationDelay: "160ms" }}
            >
              Nexora empowers students and educators with intelligent analytics,
              personalized insights, and real-time performance tracking to achieve
              academic excellence.
            </p>

            <div
              className="fade-up mt-8 flex w-full flex-col items-center gap-3 sm:w-auto sm:flex-row"
              style={{ animationDelay: "240ms" }}
            >
              <button
                onClick={navigateSignupPage}
                className="inline-flex min-h-12 items-center justify-center gap-2 rounded-lg bg-linear-to-r from-sky-700 to-sky-950 px-6 py-3 font-semibold text-white shadow-lg shadow-sky-900/20 transition duration-300 hover:-translate-y-0.5 hover:shadow-xl focus:outline-none focus:ring-4 focus:ring-sky-300"
              >
                Get Started
                <FaArrowRight />
              </button>

              <button
                onClick={NavigateSigninPage}
                className="inline-flex min-h-12 items-center justify-center gap-2 rounded-lg border border-sky-200 bg-white/80 px-6 py-3 font-semibold text-sky-950 shadow-sm transition duration-300 hover:-translate-y-0.5 hover:border-sky-400 hover:bg-white focus:outline-none focus:ring-4 focus:ring-sky-200"
              >
                Login
                <SlLogin />
              </button>
            </div>

            <div
              className="fade-up mt-10 flex w-full max-w-md items-center divide-x divide-sky-200/70"
              style={{ animationDelay: "320ms" }}
            >
              {STATS.map(([value, label]) => (
                <div key={label} className="flex-1 px-4 text-center first:pl-0 last:pr-0">
                  <p className="text-2xl font-black text-sky-800 sm:text-3xl">{value}</p>
                  <p className="mt-1 text-[10px] font-semibold uppercase tracking-wide text-slate-medium sm:text-xs">
                    {label}
                  </p>
                </div>
              ))}
            </div>
          </div>

          <div className="fade-up relative mx-auto w-full max-w-md lg:max-w-none" style={{ animationDelay: "200ms" }}>
            <div className="absolute -top-5 right-4 z-20 flex items-center gap-2 rounded-xl border border-sky-100 bg-white px-4 py-2 text-sm font-bold text-sky-800 shadow-lg shadow-sky-900/10 sm:-right-6">
              <FaArrowTrendUp className="text-sky-600" />
              +18% this term
            </div>

            <div className="relative overflow-hidden rounded-2xl border border-white/70 bg-white/70 p-6 shadow-xl shadow-sky-900/10 backdrop-blur">
              <div className="mb-5 flex items-start justify-between">
                <div>
                  <p className="text-label font-bold uppercase tracking-wider text-slate-medium">
                    Performance Overview
                  </p>
                  <p className="mt-0.5 text-sm font-bold text-sky-950">
                    Class Average Trend
                  </p>
                </div>
                <span className="inline-flex items-center gap-1.5 rounded-full bg-sky-50 px-2.5 py-1 text-label font-bold text-sky-700">
                  <span className="relative flex h-1.5 w-1.5">
                    <span className="absolute inline-flex h-full w-full animate-ping rounded-full bg-sky-500 opacity-75" />
                    <span className="relative inline-flex h-1.5 w-1.5 rounded-full bg-sky-600" />
                  </span>
                  Live
                </span>
              </div>

              <svg viewBox="0 0 300 140" className="w-full" preserveAspectRatio="none">
                <defs>
                  <linearGradient id="heroTrendFill" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="0%" stopColor="#0369a1" stopOpacity="0.25" />
                    <stop offset="100%" stopColor="#0369a1" stopOpacity="0" />
                  </linearGradient>
                </defs>

                {[35, 70, 105].map((y) => (
                  <line
                    key={y}
                    x1="0"
                    x2="300"
                    y1={y}
                    y2={y}
                    stroke="#0c4a6e"
                    strokeOpacity="0.08"
                    strokeDasharray="4 4"
                  />
                ))}

                <path d={`${CHART_PATH} L296,140 L4,140 Z`} fill="url(#heroTrendFill)" stroke="none" />

                <path
                  d={CHART_PATH}
                  fill="none"
                  stroke="#075985"
                  strokeWidth="3"
                  strokeLinecap="round"
                  className={mounted ? "draw-trend" : ""}
                  style={{ strokeDashoffset: mounted ? undefined : 420 }}
                />

                <circle cx="296" cy="10" r="8" fill="#0369a1" opacity="0.15" />
                <circle cx="296" cy="10" r="4" fill="#075985" />
              </svg>

              <div className="mt-4 space-y-3">
                {METRICS.map((m, i) => (
                  <div key={m.label}>
                    <div className="mb-1 flex items-center justify-between text-xs font-semibold text-slate-medium">
                      <span>{m.label}</span>
                      <span className="text-sky-800">{m.value}%</span>
                    </div>
                    <div className="h-1.5 w-full overflow-hidden rounded-full bg-sky-100">
                      <div
                        className="h-full rounded-full bg-linear-to-r from-sky-700 to-sky-950 transition-all ease-out"
                        style={{
                          width: mounted ? `${m.value}%` : "0%",
                          transitionDuration: "1000ms",
                          transitionDelay: `${300 + i * 150}ms`,
                        }}
                      />
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>

        <div className="absolute bottom-6 left-1/2 flex -translate-x-1/2 flex-col items-center text-sky-700/70">
          <FaArrowDown className="animate-bounce" size={35} />
        </div>
      </section>
    </>
  );
}