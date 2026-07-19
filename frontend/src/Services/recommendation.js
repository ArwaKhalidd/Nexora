import api from "@/Api/axios";

export async function getRecommendation() {
  const response = await api.get("/api/Recommendations/resources");
  return response.data;
}
