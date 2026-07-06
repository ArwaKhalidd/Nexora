import api from "@/Api/axios";

export async function getCourses() {
  const response = await api.get(`/api/courses`);
  return response.data;
}
// export async function getCourse(id) {
//   const res = await api.get(`/api/courses/${id}`);
//   return res.data;
// }
// export async function createCourse(data) {
//   const res = await api.post(`/api/courses`, data);
//   return res.data;
// }
