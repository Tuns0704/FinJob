import { useContext } from "react";
import { Outlet, useLocation } from "react-router-dom";
import { authContext } from "../cores/context/auth";
import { Navbar, Footer } from "../cores/components";

const AppContainer = () => {
	const {
		state: { isAuthenticated },
	} = useContext(authContext);
	const { pathname } = useLocation();
	return (
		<div>
			{isAuthenticated && <Navbar />}
			<div
				className={`min-h-[85vh] bg-gray-100 ${isAuthenticated ? "pt-20" : ""}`}
			>
				<Outlet />
			</div>
			{isAuthenticated && <Footer />}
		</div>
	);
};

export default AppContainer;
