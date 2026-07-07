import { sidebarLinksForAdmin, sidebarLinksForStudent } from "@/constants";
import { NavLink, useLocation } from "react-router-dom";
import Logo from "./Logo";
import { getCurrentUser } from "@/Services/user";
import { useEffect, useState } from "react";
import SidebarLink from "./SidebarLink";

const SidebarLg = () => {
  const [user, setUser] = useState({});
  const pathName = useLocation();

  useEffect(() => {
    getCurrentUser().then((res) => {
      if (res) {
        setUser(res);
      }
    });
  }, []);

  return (
    <aside className="hidden md:flex md:flex-col md:items-center md:gap-[22px]  fixed top-0 left-0 h-screen w-[230px] p-4 pt-5 bg-white border-e border-e-[#C3C6D7] shadow-sm">
      <Logo />
      <ul className=" w-full flex flex-col gap-4 mt-4">
        {user.role === "Student"
          ? sidebarLinksForStudent.map((link) => {
              const Icon = link.icon;
              return user ? (
                <SidebarLink
                  key={link.path}
                  link={link}
                  pathName={pathName}
                  Icon={Icon}
                />
              ) : (
                <div className="animate-pulse bg-slate-light h-10 rounded-lg w-full "></div>
              );
            })
          : sidebarLinksForAdmin.map((link) => {
              const Icon = link.icon;
              return user ? (
                <SidebarLink
                  key={link.path}
                  link={link}
                  pathName={pathName}
                  Icon={Icon}
                />
              ) : (
                <div className="animate-pulse bg-slate-light h-10 rounded-lg w-full "></div>
              );
            })}
      </ul>
    </aside>
  );
};

export default SidebarLg;
