import DashboardLayout from "@/mainLayout/DashboardLayout";
import ProgressCard from "@/Components/layout/ProgressCard";
import PomodoroClock from "@/Components/layout/PomodoroClock";
import TodoList from "@/Components/layout/ToDoList";
import MainChart from "@/Components/layout/MainChart";

const MyDashboard = () => {
  return (
    <DashboardLayout>
      <div className="w-full h-[89vh] grid grid-cols-12 gap-6">
        <div className="col-span-9 border-white/70 shadow-xl  shadow-sky-900/10 backdrop-blur text-sky-950 py-5 rounded-xl">
          <div className="flex gap-2">
            <MainChart />
            <ProgressCard />
          </div>
          <div className="flex flex-col items-start justify-center mt-5">
            <h1 className="text-sky-800 font-bold font-mono text-2xl">sayed</h1>
          </div>
        </div>

        {/* Right Section */}
        <div className="col-span-3 p-2 rounded-xl flex flex-col gap-6">
          <TodoList />
          <PomodoroClock />
        </div>
      </div>
    </DashboardLayout>
  );
};

export default MyDashboard;
