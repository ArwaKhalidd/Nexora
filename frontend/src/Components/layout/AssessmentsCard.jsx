import { Link } from "react-router-dom";
import { CalendarDays, BookOpen, Users } from "lucide-react";

const AssessmentCard = ({ assessment, isTutor = false, onAddQuestions }) => {
  return (
    <div className="w-[95%] rounded-2xl border border-slate-200 bg-sky-50 p-6 shadow-sm transition-all duration-300 hover:-translate-y-1 hover:shadow-lg">
      {/* Header */}
      <div className="flex flex-col justify-between gap-4 md:flex-row md:items-center">
        <div>
          <h2 className="text-2xl font-bold text-sky-900">
            {assessment.assessmentType}
          </h2>

          <p className="mt-2 flex items-center gap-2 text-slate-500">
            <CalendarDays size={18} />
            {assessment.date}
          </p>
        </div>

        <div className="rounded-lg bg-sky-100 px-4 py-2 text-sm font-semibold text-sky-900">
          Assessment #{assessment.idAssessment}
        </div>
      </div>

      {/* Information */}
      <div className="mt-8 grid gap-6 md:grid-cols-2 lg:grid-cols-4">
        <div>
          <p className="text-sm text-slate-500">Module</p>
          <p className="mt-1 font-semibold">{assessment.codeModule}</p>
        </div>

        <div>
          <p className="text-sm text-slate-500">Presentation</p>
          <p className="mt-1 font-semibold">{assessment.codePresentation}</p>
        </div>

        <div>
          <p className="flex items-center gap-2 text-sm text-slate-500">
            <BookOpen size={16} />
            Questions
          </p>

          <p className="mt-1 text-lg font-bold">{assessment.questionCount}</p>
        </div>

        <div>
          <p className="flex items-center gap-2 text-sm text-slate-500">
            <Users size={16} />
            Completed
          </p>

          <p className="mt-1 text-lg font-bold">
            {assessment.completedByStudents}
          </p>
        </div>
      </div>

      {/* Actions */}
      <div className="mt-8 flex flex-wrap justify-end gap-3">
        <Link
          to={`/assessments/${assessment.idAssessment}`}
          className="rounded-lg bg-sky-700 px-5 py-2 font-semibold text-white cursor-pointer duration-700 transition hover:bg-sky-800"
        >
          View Questions
        </Link>

        {isTutor && (
          <>
            <button
               onClick={() => onAddQuestions(assessment)}
              className="rounded-lg bg-green-700 px-5 py-2 font-semibold cursor-pointer duration-700 text-white transition hover:bg-green-600"
            >
              Add Quiestions
            </button>
          </>
        )}
      </div>
    </div>
  );
};

export default AssessmentCard;
