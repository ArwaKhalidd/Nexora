import { Link } from "react-router-dom";

const CourseCard = ({ course }) => {
  return (
    <Link
      to="/courses"
      className="block bg-white rounded-lg border border-[#E5E7EB] p-3 pb-1 w-full cursor-pointer hover:border-primary-container transition-colors"
    >
      <div>
        <img
          src={`${course.image}`}
          alt={`${course.title} thumbnail`}
          className="w-full h-52 object-cover rounded-t-md"
        />
      </div>

      <div className="py-4">
        <h3 className="font-semibold text-title text-slate-dark mb-1">
          {course.title}
        </h3>
        <p className="text-label text-slate-medium line-clamp-2">
          {course.description}
        </p>
        <div className="flex flex-wrap gap-2 mt-3">
          {course.skills?.map((skill, index) => (
            <span
              key={index}
              className="border border-slate-light px-2 py-1 rounded-sm text-label text-slate-medium font-normal"
            >
              {skill}
            </span>
          ))}
        </div>
      </div>
    </Link>
  );
};

export default CourseCard;
