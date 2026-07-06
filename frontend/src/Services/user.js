import api from "../Api/axios";
export const getCurrentUser = async () => {
  const token = localStorage.getItem("token");
  if (!token) {
    return;
  }

  const response = await api.get("/api/AcademicProfile", {
    headers: {
      "Accept": "*/*",
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
  });
  return response.data;
};
