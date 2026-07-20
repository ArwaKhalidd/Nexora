import api from "@/Api/axios";

export const getStudentAssements = async () => {
  const response = await api.get("/api/Assessments/student");
  return response.data;
};

export const getAvailableAssessments = async () => {
  const response = await api.get("/api/Assessments/available");
  return response.data;
};

//create assesment instructour
export const CreateAssessment = async (assessmentData) => {
  const response = await api.post("/api/Assessments", assessmentData);
  return response.data;
};

// get quistions of assessments
export const getAssesmentQuestions = async (assessmentId) => {
  const response = await api.get(`/api/Assesments/${assessmentId}/questions`);
  return response.data;
};

// add question
export const addQuestions = async (assessmentId, questions) => {
  const response = await api.post(
    `/api/Assessments/${assessmentId}/questions`,
    questions,
  );

  return response.data;
};

// submit answers
export const submitAnswers = async (assessmentId, answers) => {
  const response = await api.post(
    `/api/Assessments/${assessmentId}/submit-answers`,
    answers,
  );

  return response.data;
};
