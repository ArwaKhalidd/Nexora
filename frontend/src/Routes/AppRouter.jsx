import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "../Pages/Home";
import Signin from "../Pages/Login";
import SignUp from "../Pages/SignUp";


export default function AppRouter() {
  return (
    <BrowserRouter>
    <Routes>
      <Route path="/" element={<Home/>} />
      <Route path="/signin" element={<Signin/>} />
      <Route path="/signup" element={<SignUp/>}/>
    </Routes>
    </BrowserRouter>
  );
}
