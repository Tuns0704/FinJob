import React from "react";
import { SidebarProfile } from "../../cores/components";

const HomePage = () => {
	return (
		<div>
			<div className="bg-light w-full overflow-hidden">
				<div>
					<div className="flex">
						<SidebarProfile />
					</div>
				</div>
			</div>
		</div>
	);
};

export default HomePage;
