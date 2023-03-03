import { navLinks } from "../constants";
const Navbar = () => {
	return (
		<nav className="w-full flex py-6 justify-between items-center navbar">
			<img alt="FinJob" className="w-[124px] h-[32px]" />
			<ul className="list-none sm:flex hidden justify-center items-center flex-1">
				{navLinks.map((nav, index) => (
					<li
						key={nav.id}
						className={`font-poppins font-normal cursor-pointer text-[16px] ${
							index === navLinks.length - 1 ? "mr-0" : "mr-10"
						} text-d`}
					>
						<a href={`#${nav.id}`}>{nav.title}</a>
					</li>
				))}
			</ul>
			<div>Profile</div>
		</nav>
	);
};

export default Navbar;
