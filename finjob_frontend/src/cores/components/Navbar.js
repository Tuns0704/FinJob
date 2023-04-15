import { navLinks } from "../../constants";
import { Link, useNavigate } from "react-router-dom";
import FinJobLogo from "../../assets/FinJobLogo.png";
import { useContext } from "react";
import { authContext, logoutAction } from "../context/auth";
const Navbar = () => {
	const {
		state: { isAuthenticated, role, user },
		dispatch,
	} = useContext(authContext);

	const navigate = useNavigate();

	const handleLogout = () => {
		navigate("/");
		dispatch(logoutAction());
	};
	return (
		<div className="sm:px-16 px-6">
			<nav className="flex align-middle py-6 justify-between items-center navbar">
				<Link>
					<img alt="FinJob" src={FinJobLogo} className="w-[110px] h-[32px]" />
				</Link>
				<ul className="list-none sm:flex hidden justify-center items-center flex-1">
					{navLinks.map((nav, index) => (
						<li
							key={nav.id}
							className={`font-poppins px-5 py-2 rounded-3xl font-normal  cursor-pointer text-[16px] hover:bg-lightOrange hover:text-primary ${
								index === navLinks.length - 1 ? "mr-0" : "mr-10"
							} text-d`}
						>
							<Link to={`${nav.id}`}>{nav.title}</Link>
						</li>
					))}
				</ul>
				<div>
					{isAuthenticated ? (
						<div>
							<button className="bg-primary text-white font-poppins px-5 py-2 rounded-3xl font-normal text-[16px] hover:bg-lightOrange hover:text-primary">
								{user.name}
							</button>
							<button
								className="bg-primary text-white font-poppins px-5 py-2 rounded-3xl font-normal text-[16px] hover:bg-lightOrange hover:text-primary"
								onClick={handleLogout}
							>
								Logout
							</button>
						</div>
					) : (
						<div>
							<button
								className="bg-primary text-white font-poppins px-5 py-2 rounded-3xl font-normal text-[16px] hover:bg-lightOrange hover:text-primary"
								onClick={() => navigate("/login")}
							>
								Login
							</button>
						</div>
					)}
				</div>
			</nav>
		</div>
	);
};

export default Navbar;
