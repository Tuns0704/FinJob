import { Outlet } from "react-router-dom";
import { AdminSidebar } from "../cores/components";

const AdminContainer = () => {
	return (
		<div className="w-full h-screen">
			<div>
				<AdminSidebar />
			</div>
			<div className="pl-64 w-full bg-gray-100">
				<Outlet />
			</div>
		</div>
	);
};

export default AdminContainer;
