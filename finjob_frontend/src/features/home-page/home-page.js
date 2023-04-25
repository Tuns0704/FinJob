import { useCallback, useState, useEffect, useContext } from "react";
import { SidebarProfile } from "../../cores/components";
import { getAllJobs } from "../../services/job.service";
import { authContext } from "../../cores/context/auth";
import CardItem from "./card-item";
import CreateJobModal from "./create-job-modal";
import ReactPaginate from "react-paginate";

const HomePage = () => {
	const [data, setData] = useState([]);
	const [totalPages, setTotalPages] = useState(1);
	const [totalCount, setTotalCount] = useState(1);
	const [currentPage, setCurentPage] = useState(1);
	const [isOpen, setIsOpen] = useState(false);

	const {
		state: { user, role },
	} = useContext(authContext);

	const getData = useCallback(async () => {
		try {
			const response = await getAllJobs(currentPage);
			setData(response.data.result);
			const paginationHeader = response.headers.get("x-pagination");
			const totalPages = parseInt(
				paginationHeader
					.split(",")
					.find((item) => item.includes("TotalPages"))
					.split(":")[1]
			);
			setTotalPages(totalPages);
			const totalCount = parseInt(
				paginationHeader
					.split(",")
					.find((item) => item.includes("TotalCount"))
					.split(":")[1]
			);
			setTotalCount(totalCount);
		} catch (error) {
			console.log(error);
		}
	});

	useEffect(() => {
		getData();
	}, [getData]);

	const handlePageChange = (selected) => {
		setCurentPage(selected.selected + 1);
	};

	return (
		<div>
			<div className="bg-gray-100 w-full overflow-hidden">
				<div className="flex center w-full">
					<div className="flex">
						<SidebarProfile />
					</div>
					<div className="w-full mx-5 sm:mx-0 py-5 sm:w-3/6">
						<div className="bg-white rounded-xl h-auto px-10 py-5">
							<div className="text-2xl sm:text-3xl mb-5 font-medium">
								SearchJob
							</div>
							<div className="">
								<form action="">
									<div className="flex w-3/4 rounded-lg bg-lightTurquoise items-center p-2">
										<svg
											xmlns="http://www.w3.org/2000/svg"
											fill="none"
											viewBox="0 0 24 24"
											strokeWidth={1.5}
											stroke="currentColor"
											className="w-6 h-6 mx-2 text-secondary"
										>
											<path
												strokeLinecap="round"
												strokeLinejoin="round"
												d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z"
											/>
										</svg>
										<input
											className="bg-lightTurquoise px-3 py-2 w-full focus:outline-none rounded-lg"
											type="text"
										/>
									</div>
								</form>
								<div></div>
							</div>
							<div className="w-auto my-5">
								<div className="flex justify-center px-3 py-4 rounded-lg w-full bg-lightTurquoise text-secondary font-medium">
									{totalCount} Jobs
								</div>
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
						<div className="flex justify-center mt-5">
							<ReactPaginate
								pageCount={totalPages}
								breakLabel="..."
								nextLabel={
									<div className="p-2 ml-2 bg-primary rounded-lg">
										<svg
											xmlns="http://www.w3.org/2000/svg"
											fill="none"
											viewBox="0 0 24 24"
											strokeWidth={1.5}
											stroke="currentColor"
											className="w-6 h-6 text-white"
										>
											<path
												strokeLinecap="round"
												strokeLinejoin="round"
												d="M3 8.688c0-.864.933-1.405 1.683-.977l7.108 4.062a1.125 1.125 0 010 1.953l-7.108 4.062A1.125 1.125 0 013 16.81V8.688zM12.75 8.688c0-.864.933-1.405 1.683-.977l7.108 4.062a1.125 1.125 0 010 1.953l-7.108 4.062a1.125 1.125 0 01-1.683-.977V8.688z"
											/>
										</svg>
									</div>
								}
								previousLabel={
									<div className="p-2 mr-2 bg-primary rounded-lg">
										<svg
											xmlns="http://www.w3.org/2000/svg"
											fill="none"
											viewBox="0 0 24 24"
											strokeWidth={1.5}
											stroke="currentColor"
											className="w-6 h-6 text-white"
										>
											<path
												strokeLinecap="round"
												strokeLinejoin="round"
												d="M21 16.811c0 .864-.933 1.405-1.683.977l-7.108-4.062a1.125 1.125 0 010-1.953l7.108-4.062A1.125 1.125 0 0121 8.688v8.123zM11.25 16.811c0 .864-.933 1.405-1.683.977l-7.108-4.062a1.125 1.125 0 010-1.953L9.567 7.71a1.125 1.125 0 011.683.977v8.123z"
											/>
										</svg>
									</div>
								}
								pageRangeDisplayed={5}
								marginPagesDisplayed={2}
								onPageChange={handlePageChange}
								containerClassName={"flex gap-3"}
								activeLinkClassName={"active"}
								breakClassName={"break-me"}
								previousClassName={"prev"}
								pageClassName="flex w-10 justify-center items-center bg-white text-primary rounded-lg"
								nextClassName={"next"}
								disabledClassName={"disabled"}
							/>
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
