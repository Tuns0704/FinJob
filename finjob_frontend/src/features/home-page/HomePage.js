import { useCallback, useState, useEffect, useContext } from "react";
import { SidebarProfile } from "../../cores/components";
import { getAllJobs } from "../../services/job.service";
import { authContext } from "../../cores/context/auth";
import CardItem from "./CardItem";
import CreateJobModal from "./CreateJobModal";

const HomePage = () => {
	const [data, setData] = useState([]);
	const [totalPages, setTotalPages] = useState(1);
	const [currentPage, setCurentPage] = useState(1);
	const [isOpen, setIsOpen] = useState(false);

	const {
		state: { user, token },
	} = useContext(authContext);

	const getData = useCallback(async () => {
		try {
			const response = await getAllJobs(currentPage);
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
				<div className="flex center py-5 w-full">
					<div className="flex">
						<SidebarProfile />
					</div>
					<div className="w-full py-5 sm:w-3/6">
						<div className="bg-white rounded-xl h-60">
							<form action="">
								<input type="text" />
							</form>
						</div>
						<div>
							<button onClick={() => setIsOpen(true)}>Open Modal</button>
							{isOpen && (
								<CreateJobModal
									isOpen={isOpen}
									onClose={() => setIsOpen(false)}
								/>
							)}
						</div>
						<div className="mt-5">
							{data.map((item) => (
								<CardItem key={item.id} item={item} reload={getData} />
							))}
						</div>
					</div>
					<div className="py-10">
						<div className="bg-white rounded-xl h-60"></div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default HomePage;
