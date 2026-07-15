import { Link } from "react-router-dom";
import DropdownMenuComponent from "./DropDownMenuComponent";

const CourseCard = ({ course, onEnroll }) => {
  return (
    <div className="overflow-hidden rounded-xl border border-slate-200 bg-white shadow-sm transition-all duration-300 hover:-translate-y-1 hover:shadow-lg">
      {/* Header */}
      <div className="flex items-start justify-between border-b border-slate-100 p-5">
        <div>
          <h2 className="text-xl font-bold text-slate-900">
            {course.name}
          </h2>

          <p className="mt-1 text-sm text-slate-500">
            {course.codeModule} • {course.codePresentation}
          </p>
        </div>

        <DropdownMenuComponent />
      </div>

      {/* Description */}
      <div className="px-5 pt-4">
        <p className="line-clamp-3 text-sm text-slate-600">
          {course.description}
        </p>
      </div>

      {/* Skills */}
      <div className="flex flex-wrap gap-2 px-5 pt-4">
        {course.skills?.map((skill) => (
          <span
            key={skill}
            className="rounded-full bg-sky-100 px-3 py-1 text-xs font-medium text-sky-800"
          >
            {skill}
          </span>
        ))}
      </div>

      {/* Course Info */}
      <div className="mt-5 space-y-3 px-5">
        <div className="flex justify-between">
          <span className="text-slate-500">Tutor</span>

          <span className="font-medium">
            {course.tutor
              ? `${course.tutor.firstName} ${course.tutor.lastName}`
              : "Unknown"}
          </span>
        </div>

        <div className="flex justify-between">
          <span className="text-slate-500">Hours</span>

          <span className="font-medium">
            {course.hours ?? 0} hrs
          </span>
        </div>

        <div className="flex justify-between">
          <span className="text-slate-500">Students</span>

          <span className="font-medium">
            {course.studentInfos?.length ?? 0}
          </span>
        </div>
      </div>

      {/* Buttons */}
      <div className="flex gap-3 p-5">
        <Link
          to={`/courses/${course.codeModule}/${course.codePresentation}`}
          className="flex-1 rounded-lg border border-slate-300 py-2 text-center transition hover:bg-slate-100"
        >
          Details
        </Link>

        <button
          onClick={() =>
            onEnroll(course.codeModule, course.codePresentation)
          }
          className="flex-1 rounded-lg bg-sky-900 py-2 text-white transition hover:bg-sky-800"
        >
          Enroll
        </button>
      </div>
    </div>
  );
};

export default CourseCard;