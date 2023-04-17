import React, { useCallback, useState, useEffect } from "react";
import { SidebarProfile } from "../../cores/components";
import { getAllJobs } from "../../services/job.service";
import CardItem from "./CardItem";

const HomePage = () => {
	const [data, setData] = useState([]);

	const getData = useCallback(async () => {
		try {
			const response = await getAllJobs();
			setData(response.data.result);
		} catch (error) {
			console.log(error);
		}
	});

	useEffect(() => {
		getData();
	}, [getData]);

	return (
		<div>
			<div className="bg-gray-100 w-full overflow-hidden">
				<div className="flex center py-5">
					<div className="flex">
						<SidebarProfile />
					</div>
					<div className="w-3/6 py-5">
						<div className="bg-white rounded-xl h-60">
							<form action="">
								<input type="text" />
							</form>
						</div>
						<div className="mt-5">
							{data.map((item) => (
								<CardItem key={item.id} item={item} reload={getData} />
							))}
						</div>
					</div>
					<div className=" pl-10">
						<div className="bg-white rounded-xl h-60"></div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default HomePage;
