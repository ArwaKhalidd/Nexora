import { deleteCourse } from "@/Services/courses";
import { useState } from "react";

const DeleteCourseCard = ({ course, onClose, onSuccess }) => {
  const [loading, setLoading] = useState(false);

  if (!course) return null;

  const handleDelete = async () => {
    try {
      setLoading(true);

      await deleteCourse(
        course.codeModule,
        course.codePresentation
      );

      if (onSuccess) {
        await onSuccess();
      }

      onClose();
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex h-[35vh] w-full flex-col justify-between rounded-lg bg-white p-6 shadow-lg">
      <div>
        <h2 className="text-2xl font-bold text-red-700">
          Delete Course
        </h2>

        <p className="mt-4 text-gray-600">
          Are you sure you want to delete the this course?
        </p>

        <div className="mt-4 rounded-lg bg-red-50 p-4">
          <h3 className="text-lg font-semibold text-sky-900">
            {course.name}
          </h3>

          <p className="mt-1 text-sm text-gray-600">
            {course.codeModule} • {course.codePresentation}
          </p>
        </div>

        <p className="mt-4 text-sm font-medium text-red-500">
         cannot be undone.
        </p>
      </div>

      <div className="mt-4 flex justify-end gap-4">
        <button
          type="button"
          onClick={onClose}
          disabled={loading}
          className="rounded-lg border cursor-pointer border-gray-300 px-6 py-2 font-semibold transition hover:bg-gray-100 disabled:cursor-not-allowed duration-500"
        >
          Cancel
        </button>

        <button
          type="button"
          onClick={handleDelete}
          disabled={loading}
          className="rounded-lg bg-red-700 px-6 py-2 cursor-pointer font-semibold text-white transition hover:bg-red-600 disabled:cursor-not-allowed duration-500 disabled:bg-red-400"
        >
          {loading ? "Deleting..." : "Delete"}
        </button>
      </div>
    </div>
  );
};

export default DeleteCourseCard;