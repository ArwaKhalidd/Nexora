import { useState, useEffect } from "react";

export function useData() {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("/data/dashboard-data.json")
      .then((res) => res.json())
      .then((d) => {
        setData(d);
        setLoading(false);
      })
      .catch((err) => {
        console.error("Failed to load data:", err);
        setLoading(false);
      });
  }, []);

  return { data, loading };
}

export function filterStudents(students, filters) {
  return students.filter((student) => {
    if (
      filters.presentation !== "All" &&
      student.pres !== filters.presentation
    ) {
      return false;
    }

    if (
      filters.region !== "All" &&
      student.reg !== filters.region
    ) {
      return false;
    }

    if (
      filters.module !== "All" &&
      student.mod !== filters.module
    ) {
      return false;
    }

    return true;
  });
}

export function getFilterOptions(students) {
  return {
    presentations: [
      "All",
      ...new Set(students.map((student) => student.pres)),
    ].sort(),

    regions: [
      "All",
      ...new Set(students.map((student) => student.reg)),
    ].sort(),

    modules: [
      "All",
      ...new Set(students.map((student) => student.mod)),
    ].sort(),
  };
}