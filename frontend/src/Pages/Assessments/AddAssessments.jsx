import DashboardLayout from "@/mainLayout/DashboardLayout";
import { CreateAssessment } from "@/Services/Assessments";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { SuccessFlash, ErrorFlash } from "@/Components/UI/FlashMessages";

const AddAssessments = () => {
  const { codeModule, codePresentation } = useParams();
  const [assessmentType, setAssessmentType] = useState("");
  const [date, setDate] = useState("");
  const [flash, setFlash] = useState({
    message: "",
    type: "",
    show: false,
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const body = {
        codeModule,
        codePresentation,
        assessmentType,
        date,
      };

      const res = await CreateAssessment(body);

      console.log(res);
      setFlash({
        message: "Assessment Created Successfully",
        type: "success",
        show: true,
      });
    } catch (err) {
      setFlash({ message: `${err.response.data.message}`, type: "error", show: true });
      console.log(err.response)
      console.log(err.response.data);
      console.log(err.response.status);
    }
  };

  useEffect(() => {
    if (!flash.show) return;

    const timer = setTimeout(() => {
      setFlash({
        type: "",
        show: false,
        message: "",
      });
    }, 2500);

    return () => clearTimeout(timer);
  }, [flash.show]);
  
  return (
    <>
      {flash.show &&
        (flash.type === "error" ? (
          <ErrorFlash content={flash.message} />
        ) : (
          <SuccessFlash content={flash.message} />
        ))}
      <DashboardLayout>
        <div className="mx-auto mt-10 max-w-xl ">
          <h1 className="text-sky-800 font-bold font-serif">Add Assessment</h1>
          <form onSubmit={handleSubmit} className="space-y-4">
            <input
              type="text"
              placeholder="Assessment Type"
              value={assessmentType}
              onChange={(e) => setAssessmentType(e.target.value)}
              className="w-full rounded  border border-gray-300 p-3"
            />
            <input
              type="date"
              value={date}
              onChange={(e) => setDate(e.target.value)}
              className="w-full rounded  border border-gray-300 p-3"
            />
            <button className="w-full rounded-md transition-all duration-700 cursor-pointer hover:bg-green-700  bg-sky-700 p-3 text-white">
              Create Assessment
            </button>
          </form>
        </div>
      </DashboardLayout>
    </>
  );
};
export default AddAssessments;
