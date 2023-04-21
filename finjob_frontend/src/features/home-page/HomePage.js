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
		state: { user, role },
	} = useContext(authContext);

	const getData = useCallback(async () => {
		try {
			const response = await getAllJobs(currentPage);
			setData(response.data.result);
			console.log(response.headers["x-pagination"]);
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
				<div className="flex center w-full">
					<div className="flex">
						<SidebarProfile />
					</div>
					<div className="w-full py-5 sm:w-3/6">
						<div className="bg-white rounded-xl h-48">
							<div>
								<form action="">
									<input className="" type="text" />
								</form>
							</div>
							<div>
								{role === "BusinessEmployer" && (
									<>
										<div className="w-full">
											<div className="flex justify-end">
												<button
													onClick={() => setIsOpen(true)}
													className="bg-secondary hover:bg-cyan-600 text-white py-2 px-4 rounded"
												>
													Create Job
												</button>
											</div>
										</div>
										{isOpen && (
											<CreateJobModal
												isOpen={isOpen}
												onClose={() => setIsOpen(false)}
											/>
										)}
									</>
								)}
							</div>
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
