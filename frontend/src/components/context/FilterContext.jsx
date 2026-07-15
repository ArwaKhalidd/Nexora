import { createContext, useContext, useState } from "react";

const FilterContext = createContext(null);

export function FilterProvider({ children }) {
  const [filters, setFilters] = useState({
    presentation: "All",
    region: "All",
    module: "All",
  });

  const [currentPage, setCurrentPage] = useState("executive");

  return (
    <FilterContext.Provider
      value={{
        filters,
        setFilters,
        currentPage,
        setCurrentPage,
      }}
    >
      {children}
    </FilterContext.Provider>
  );
}

export function useFilters() {
  const ctx = useContext(FilterContext);

  if (!ctx) {
    throw new Error("useFilters must be used within FilterProvider");
  }

  return ctx;
}