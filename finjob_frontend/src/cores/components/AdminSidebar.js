import { useContext } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { authContext, logoutAction } from "../context/auth";
import logo from "../../assets/FinJobLogo.png";

const AdminSidebar = () => {
	const navigate = useNavigate();
	const { pathname } = useLocation();
	const {
		state: { user },
		dispatch,
	} = useContext(authContext);

	return (
		<aside
			id="logo-sidebar"
			className="fixed left-0 z-40 w-64 h-screen transition-transform -translate-x-full sm:translate-x-0"
			aria-label="Sidebar"
		>
			<div className="h-full pt-10 px-3 pb-4 overflow-y-auto bg-white">
				<Link to="/" className="flex items-center pl-2.5 mb-5">
					<img src={logo} className="w-28" alt="FinJob Logo" />
					<span className="self-center text-xl font-semibold whitespace-nowrap dark:text-white">
						FinJob
					</span>
				</Link>
				<ul className="space-y-2 font-medium">
					<li>
						<Link
							to="/"
							className="flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100"
						>
							<svg
								aria-hidden="true"
								className="w-6 h-6 text-primary transition duration-75"
								fill="currentColor"
								viewBox="0 0 20 20"
								xmlns="http://www.w3.org/2000/svg"
							>
								<path d="M2 10a8 8 0 018-8v8h8a8 8 0 11-16 0z"></path>
								<path d="M12 2.252A8.014 8.014 0 0117.748 8H12V2.252z"></path>
							</svg>
							<span className="ml-3">Dashboard</span>
						</Link>
					</li>
					<li>
						<Link
							href="#"
							className="flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100"
						>
							<svg
								aria-hidden="true"
								className="flex-shrink-0 w-6 h-6 text-primary transition duration-75 group-hover:text-gray-900"
								fill="currentColor"
								viewBox="0 0 20 20"
								xmlns="http://www.w3.org/2000/svg"
							>
								<path d="M5 3a2 2 0 00-2 2v2a2 2 0 002 2h2a2 2 0 002-2V5a2 2 0 00-2-2H5zM5 11a2 2 0 00-2 2v2a2 2 0 002 2h2a2 2 0 002-2v-2a2 2 0 00-2-2H5zM11 5a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2V5zM11 13a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2v-2z"></path>
							</svg>
							<span className="flex-1 ml-3 whitespace-nowrap">Kanban</span>
						</Link>
					</li>
					<li>
						<Link
							href="#"
							className="flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100"
						>
							<svg
								aria-hidden="true"
								className="flex-shrink-0 w-6 h-6 text-primary transition duration-75 group-hover:text-gray-900"
								fill="currentColor"
								viewBox="0 0 20 20"
								xmlns="http://www.w3.org/2000/svg"
							>
								<path d="M8.707 7.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l2-2a1 1 0 00-1.414-1.414L11 7.586V3a1 1 0 10-2 0v4.586l-.293-.293z"></path>
								<path d="M3 5a2 2 0 012-2h1a1 1 0 010 2H5v7h2l1 2h4l1-2h2V5h-1a1 1 0 110-2h1a2 2 0 012 2v10a2 2 0 01-2 2H5a2 2 0 01-2-2V5z"></path>
							</svg>
							<span className="flex-1 ml-3 whitespace-nowrap">Inbox</span>
						</Link>
					</li>
					<li>
						<Link
							href="#"
							className="flex items-center p-2 text-gray-900 rounded-lg  hover:bg-gray-100"
						>
							<svg
								aria-hidden="true"
								className="flex-shrink-0 w-6 h-6 text-primary transition duration-75 group-hover:text-gray-900"
								fill="currentColor"
								viewBox="0 0 20 20"
								xmlns="http://www.w3.org/2000/svg"
							>
								<path
									fillRule="evenodd"
									d="M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z"
									clipRule="evenodd"
								></path>
							</svg>
							<span className="flex-1 ml-3 whitespace-nowrap">Users</span>
						</Link>
					</li>
					<li>
						<Link
							href="#"
							className="flex items-center p-2 text-gray-900 rounded-lg hover:bg-gray-100"
						>
							<svg
								aria-hidden="true"
								className="flex-shrink-0 w-6 h-6 text-primary transition duration-75 group-hover:text-gray-900"
								fill="currentColor"
								viewBox="0 0 20 20"
								xmlns="http://www.w3.org/2000/svg"
							>
								<path
									fillRule="evenodd"
									d="M10 2a4 4 0 00-4 4v1H5a1 1 0 00-.994.89l-1 9A1 1 0 004 18h12a1 1 0 00.994-1.11l-1-9A1 1 0 0015 7h-1V6a4 4 0 00-4-4zm2 5V6a2 2 0 10-4 0v1h4zm-6 3a1 1 0 112 0 1 1 0 01-2 0zm7-1a1 1 0 100 2 1 1 0 000-2z"
									clipRule="evenodd"
								></path>
							</svg>
							<span className="flex-1 ml-3 whitespace-nowrap">Products</span>
						</Link>
					</li>
				</ul>
			</div>
		</aside>
	);
};

export default AdminSidebar;
