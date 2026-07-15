import { updateCourse } from "@/Services/courses";
import { useEffect, useState } from "react";

const EditCourseForm = ({ course, onSuccess, onClose }) => {
  const [formData, setFormData] = useState({
    name: "",
    description: "",
    hours: "",
    skills: "",
  });

  useEffect(() => {
    if (!course) return;

    setFormData({
      name: course.name,
      description: course.description,
      hours: course.hours,
      skills: course.skills.join(", "),
    });
  }, [course]);

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleUpdateCourse = async (e) => {
    e.preventDefault();

    try {
      await updateCourse(course.codeModule, course.codePresentation, {
        name: formData.name,
        description: formData.description,
        hours: Number(formData.hours),
        skills: formData.skills
          .split(",")
          .map((skill) => skill.trim())
          .filter((skill) => skill !== ""),
      });

      if (onSuccess) {
        await onSuccess();
      }
    } catch (error) {
      console.log("Something went wrong");
    }
  };

  if (!course) return null;

  return (
    <form
      onSubmit={handleUpdateCourse}
      className="h-[90vh] md:h-[75vh] text-sky-950 font-bold space-y-2.5 px-5"
    >
      <h2 className="text-xl md:text-2xl font-extrabold bg-linear-to-r from-sky-700 to-sky-950 bg-clip-text text-transparent">
        Edit Course
      </h2>

      <input
        type="text"
        value={course.codeModule}
        readOnly
        className="w-full rounded-lg border p-3 bg-gray-100"
      />

      <input
        type="text"
        value={course.codePresentation}
        readOnly
        className="w-full rounded-lg border p-3 bg-gray-100"
      />

      <input
        type="text"
        name="name"
        placeholder="Course Name"
        value={formData.name}
        onChange={handleChange}
        className="w-full rounded-lg border p-3"
      />

      <textarea
        name="description"
        placeholder="Course Description"
        value={formData.description}
        onChange={handleChange}
        rows={5}
        className="w-full rounded-lg border p-3"
      />

      <input
        type="number"
        name="hours"
        placeholder="Hours"
        value={formData.hours}
        onChange={handleChange}
        className="w-full rounded-lg border p-3"
      />

      <input
        type="text"
        name="skills"
        placeholder="React, JavaScript, HTML"
        value={formData.skills}
        onChange={handleChange}
        className="w-full rounded-lg border p-3"
      />

      <div className="w-[90%] gap-5 flex items-center justify-between">
        <button
        type="submit"
        className="w-[45%] mt-10 md:mt-5 rounded-lg cursor-pointer hover:bg-green-700 transition-colors duration-700 bg-sky-900 py-3 text-white font-semibold"
      >
        Update Course
      </button>
      <button
        type="button"
        onClick={onClose}
        className="w-[45%] mt-10 md:mt-5 rounded-lg cursor-pointer hover:bg-red-700 transition-colors duration-700 bg-sky-900 py-3 text-white font-semibold"
      >
        Cancel
      </button>
      </div>
    </form>
  );
};

export default EditCourseForm;
