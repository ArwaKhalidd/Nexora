import DashboardLayout from "@/mainLayout/DashboardLayout";
import { enrolled } from "@/Services/courses";
import { useState } from "react";

const MyCourses = () => {
  const [loading, setLoading] = useState(true);
  const [courses, setCourses] = useState([]);

  const loadMyCourses = async () => {
    try{
      const data = await enrolled();
      setCourses(data);
    }finally{
      setLoading(false)
    }
  }

  useEffect(() => {
    loadMyCourses();
  }, []);
  return <DashboardLayout>my courses</DashboardLayout>;
};

export default MyCourses;
