import { NavLink, useNavigate } from "react-router-dom";
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
		<nav className="fixed top-0 w-full bg-white">
			<div className="sm:px-16 px-6">
				<div className="flex align-middle py-5 justify-between items-center navbar">
					<NavLink to="/">
						<img alt="FinJob" src={FinJobLogo} className="w-[110px] h-[32px]" />
					</NavLink>
					<ul className="sm:flex hidden justify-center items-center flex-1 gap-2">
						<li>
							<NavLink
								to="/"
								className="flex gap-3 items-center px-5 py-2 rounded-3xl font-normal  cursor-pointer text-[16px]  hover:bg-lightOrange hover:text-primary text-dark"
							>
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth={1.5}
									stroke="currentColor"
									className="w-6 h-6"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M2.25 12l8.954-8.955c.44-.439 1.152-.439 1.591 0L21.75 12M4.5 9.75v10.125c0 .621.504 1.125 1.125 1.125H9.75v-4.875c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21h4.125c.621 0 1.125-.504 1.125-1.125V9.75M8.25 21h8.25"
									/>
								</svg>
								<div className="hidden lg:flex">Home</div>
							</NavLink>
						</li>
						<li>
							<NavLink
								to="/finjob"
								className="flex gap-3 items-center px-5 py-2 rounded-3xl font-normal  cursor-pointer text-[16px]  hover:bg-lightOrange hover:text-primary text-dark"
							>
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth={1.5}
									stroke="currentColor"
									className="w-6 h-6"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M20.25 14.15v4.25c0 1.094-.787 2.036-1.872 2.18-2.087.277-4.216.42-6.378.42s-4.291-.143-6.378-.42c-1.085-.144-1.872-1.086-1.872-2.18v-4.25m16.5 0a2.18 2.18 0 00.75-1.661V8.706c0-1.081-.768-2.015-1.837-2.175a48.114 48.114 0 00-3.413-.387m4.5 8.006c-.194.165-.42.295-.673.38A23.978 23.978 0 0112 15.75c-2.648 0-5.195-.429-7.577-1.22a2.016 2.016 0 01-.673-.38m0 0A2.18 2.18 0 013 12.489V8.706c0-1.081.768-2.015 1.837-2.175a48.111 48.111 0 013.413-.387m7.5 0V5.25A2.25 2.25 0 0013.5 3h-3a2.25 2.25 0 00-2.25 2.25v.894m7.5 0a48.667 48.667 0 00-7.5 0M12 12.75h.008v.008H12v-.008z"
									/>
								</svg>
								<div className="hidden lg:flex">Find Job</div>
							</NavLink>
						</li>
						<li>
							<NavLink
								to="/messages"
								className="flex gap-3 items-center px-5 py-2 rounded-3xl font-normal  cursor-pointer text-[16px]  hover:bg-lightOrange hover:text-primary text-dark"
							>
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth={1.5}
									stroke="currentColor"
									className="w-6 h-6"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M8.625 12a.375.375 0 11-.75 0 .375.375 0 01.75 0zm0 0H8.25m4.125 0a.375.375 0 11-.75 0 .375.375 0 01.75 0zm0 0H12m4.125 0a.375.375 0 11-.75 0 .375.375 0 01.75 0zm0 0h-.375M21 12c0 4.556-4.03 8.25-9 8.25a9.764 9.764 0 01-2.555-.337A5.972 5.972 0 015.41 20.97a5.969 5.969 0 01-.474-.065 4.48 4.48 0 00.978-2.025c.09-.457-.133-.901-.467-1.226C3.93 16.178 3 14.189 3 12c0-4.556 4.03-8.25 9-8.25s9 3.694 9 8.25z"
									/>
								</svg>
								<div className="hidden lg:flex">Messages</div>
							</NavLink>
						</li>
						<li>
							<NavLink
								to={`profile/${user.id}`}
								className="flex gap-3 items-center px-5 py-2 rounded-3xl font-normal  cursor-pointer text-[16px]  hover:bg-lightOrange hover:text-primary text-dark"
							>
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth={1.5}
									stroke="currentColor"
									className="w-6 h-6"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M17.982 18.725A7.488 7.488 0 0012 15.75a7.488 7.488 0 00-5.982 2.975m11.963 0a9 9 0 10-11.963 0m11.963 0A8.966 8.966 0 0112 21a8.966 8.966 0 01-5.982-2.275M15 9.75a3 3 0 11-6 0 3 3 0 016 0z"
									/>
								</svg>
								<div className="hidden lg:flex">Profile</div>
							</NavLink>
						</li>
						<li>
							<NavLink
								to="/overview"
								className="flex gap-3 items-center px-5 py-2 rounded-3xl font-normal  cursor-pointer text-[16px]  hover:bg-lightOrange hover:text-primary text-d"
							>
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth={1.5}
									stroke="currentColor"
									className="w-6 h-6"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M9 12h3.75M9 15h3.75M9 18h3.75m3 .75H18a2.25 2.25 0 002.25-2.25V6.108c0-1.135-.845-2.098-1.976-2.192a48.424 48.424 0 00-1.123-.08m-5.801 0c-.065.21-.1.433-.1.664 0 .414.336.75.75.75h4.5a.75.75 0 00.75-.75 2.25 2.25 0 00-.1-.664m-5.8 0A2.251 2.251 0 0113.5 2.25H15c1.012 0 1.867.668 2.15 1.586m-5.8 0c-.376.023-.75.05-1.124.08C9.095 4.01 8.25 4.973 8.25 6.108V8.25m0 0H4.875c-.621 0-1.125.504-1.125 1.125v11.25c0 .621.504 1.125 1.125 1.125h9.75c.621 0 1.125-.504 1.125-1.125V9.375c0-.621-.504-1.125-1.125-1.125H8.25zM6.75 12h.008v.008H6.75V12zm0 3h.008v.008H6.75V15zm0 3h.008v.008H6.75V18z"
									/>
								</svg>
								<div className="hidden lg:flex">Overview</div>
							</NavLink>
						</li>
					</ul>
					<div>
						{isAuthenticated && (
							<div>
								<button
									className="bg-primary text-white px-5 py-2 rounded-3xl font-normal text-[16px] hover:bg-lightOrange hover:text-primary"
									onClick={handleLogout}
								>
									Logout
								</button>
							</div>
						)}
					</div>
				</div>
			</div>
		</nav>
	);
};

export default Navbar;
