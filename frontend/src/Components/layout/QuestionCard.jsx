const QuestionCard = ({ question }) => {
  return (
    <div className="h-full rounded-2xl border border-slate-200 bg-white p-6 shadow-sm transition-all duration-300 hover:-translate-y-1 hover:shadow-lg">
      {/* Header */}
      <div className="flex items-start justify-between gap-3">
        <h2 className="text-lg font-bold text-sky-900">
          {question.text}
        </h2>

        <span className="rounded-lg bg-sky-100 px-3 py-1 text-sm font-semibold text-sky-800 whitespace-nowrap">
          {question.points} Point
        </span>
      </div>

      <p className="mt-3 text-sm text-slate-500">
        Type: {question.questionType}
      </p>

      {/* Options */}
      <div className="mt-6 space-y-3">
        {question.options.map((option) => (
          <div
            key={option.id}
            className={`rounded-lg border p-3 transition ${
              option.isCorrect
                ? "border-green-500 bg-green-50"
                : "border-slate-200 bg-slate-50"
            }`}
          >
            <div className="flex items-center justify-between gap-3">
              <span className="text-sm text-slate-700">
                {option.text}
              </span>

              {option.isCorrect && (
                <span className="rounded-full bg-green-600 px-2 py-1 text-xs font-semibold text-white">
                  Correct
                </span>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default QuestionCard;