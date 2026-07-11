import DashboardLayout from "@/mainLayout/DashboardLayout";
import ProgressCard from "@/Components/layout/ProgressCard";

const MyDashboard = () => {
  return (
    <DashboardLayout>
      <div className="w-full h-full">
        {/*top*/}
        <div className="flex items-center justify-end ">
          <ProgressCard />
        </div>
        {/*bottom*/}
      </div>
    </DashboardLayout>
  );
};

export default MyDashboard;
