import { Outlet } from "react-router-dom";
import { AdminSidebar } from "../cores/components";

const AdminContainer = () => {
	return (
		<div className="w-full">
			<div>
				<AdminSidebar />
			</div>
			<div>
				<Outlet />
			</div>
		</div>
	);
};

export default AdminContainer;
