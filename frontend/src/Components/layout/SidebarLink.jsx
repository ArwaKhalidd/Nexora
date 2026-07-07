import { NavLink } from "react-router-dom";

const SidebarLink = ({ link, pathName, Icon }) => {
  return (
    <NavLink
      key={link.path}
      to={link.path}
      className={` flex items-center gap-3 py-2.5 px-3 transition-colors rounded-lg cursor-pointer ${
        pathName.pathname === link.path
          ? "bg-primary-container"
          : "hover:bg-primary-container/10"
      }`}
    >
      <Icon
        className={`${pathName.pathname === link.path ? "text-white " : "text-dark"} h-5 w-5`}
      />
      <p
        className={`text-nav ${
          pathName.pathname === link.path
            ? "text-white font-medium"
            : "text-slate-dark font-bold"
        }`}
      >
        {link.label}
      </p>
    </NavLink>
  );
};

export default SidebarLink;
