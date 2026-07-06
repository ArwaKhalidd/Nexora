import CourseCard from "@/Components/layout/CourseCard";
import MainHeading from "@/Components/layout/MainHeading";
import { courses } from "@/constants";
import DashboardLayout from "@/mainLayout/DashboardLayout";
import React from "react";

const Courses = () => {
  return (
    <DashboardLayout>
      <div className="flex flex-col items-start lg:items-center justify-between sm:flex-row flex-wrap">
        <MainHeading
          title="Courses"
          description="Here's an overview of your courses"
        />

        <div className="flex flex-col lg:flex-row gap-4">
          <input
            type="text"
            placeholder="Search courses..."
            className="w-[300px] border border-slate-light px-4 py-2 rounded-sm text-body text-slate-medium font-normal"
          />

          <select
            name="category"
            id="category"
            className="w-[300px] border border-slate-light px-4 py-2 rounded-sm text-body text-slate-medium font-normal"
          >
            <option value="" disabled selected hidden>
              Filter by category
            </option>
            {courses.map((course) => (
              <option value={course.category}>{course.title}</option>
            ))}
          </select>
        </div>
      </div>

      {/* courses section */}
      <section className="w-full">
        {/* Course Cards */}
        <div className="w-full grid md:grid-cols-2 lg:grid-cols-3 gap-6 mt-8">
          {courses.map((course) => (
            <CourseCard key={course.id} course={course} />
          ))}
        </div>
      </section>
    </DashboardLayout>
  );
};

export default Courses;
