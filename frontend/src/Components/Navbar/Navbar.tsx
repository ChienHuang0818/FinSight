import React from "react";
import { Link } from "react-router-dom";
import logo from "./logo.png";
import "./Navbar.css";
import { useAuth } from "../../Context/useAuth";

interface Props {}

const Navbar = (props: Props) => {
  const { isLoggedIn, user, logout } = useAuth();
  return (
    <nav className="navbar-container">
      <div className="navbar-inner">
        <div className="navbar-section navbar-section-left">
          <Link to="/">
            <img src={logo} alt="" className="navbar-logo" />
          </Link>
        </div>
        {isLoggedIn() ? (
          <div className="navbar-section navbar-section-right hidden lg:flex">
            <div className="navbar-welcome">Welcome, {user?.userName}</div>
            <a onClick={logout} className="navbar-button navbar-button-primary">
              Logout
            </a>
          </div>
        ) : (
          <div className="navbar-section navbar-section-right hidden lg:flex">
            <div className="hidden font-bold lg:flex">
              <Link to="/search" className="navbar-link">
                Search
              </Link>
              <Link to="/login" className="navbar-link">
                Login
              </Link>
            </div>
            <Link to="/register" className="navbar-button navbar-button-primary">
              Signup
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
