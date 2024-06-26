import { useCallback, useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { authContext } from "../../cores/context/auth";

const AdminManageUser = () => {
	const [data, setData] = useState([]);
	const [totalPages, setTotalPages] = useState(1);
	const [currentPage, setCurentPage] = useState(1);
	const [pageSize, setPageSize] = useState(10);

	const {
		state: { user, token },
	} = useContext(authContext);

	const navigate = useNavigate();

	// const getData = useCallback(async () => {
	//   try {
	//     const response = await (currentPage, "", token);
	//     setData(response.data.result);
	//     console.log(response.data.result);
	//     setTotalPages(response.data.totalPages);
	//   } catch (error) {
	//     console.log(error);
	//   }
	// }, [currentPage, token]);

	// const removeUser = async (id) => {
	// 	const confirmed = window.confirm(
	// 		"Are you sure you want to delete this job?"
	// 	);
	// 	if (!confirmed) {
	// 		return;
	// 	}
	// 	try {
	// 		const response = await deleteJob(id);
	// 		if (response.statusCode === 204) {
	// 			toast.success("Delete job successfully!");
	// 			getData();
	// 		}
	// 	} catch (error) {
	// 		console.log(error);
	// 	}
	// };

	// useEffect(() => {
	// 	getData();
	// }, [getData]);

	return (
		<div className="container px-4 sm:px-8 min-h-screen">
			<div className="py-8">
				<div>
					<h2 className="text-2xl font-semibold leading-tight">Manage Users</h2>
				</div>
				<div className="my-2 flex sm:flex-row flex-col">
					<div className="flex flex-row mb-1 sm:mb-0">
						<div className="relative">
							<select className="appearance-none h-full rounded-l border block w-full bg-white border-gray-400 text-gray-700 py-2 px-4 pr-8 leading-tight focus:outline-none focus:bg-white focus:border-gray-500">
								<option>5</option>
								<option>10</option>
								<option>20</option>
							</select>
							<div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
								<svg
									className="fill-current h-4 w-4"
									xmlns="http://www.w3.org/2000/svg"
									viewBox="0 0 20 20"
								>
									<path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
								</svg>
							</div>
						</div>
						<div className="relative">
							<select className="h-full rounded-r border-t sm:rounded-r-none sm:border-r-0 border-r border-b block appearance-none w-full bg-white border-gray-400 text-gray-700 py-2 px-4 pr-8 leading-tight focus:outline-none focus:border-l focus:border-r focus:bg-white focus:border-gray-500">
								<option>All</option>
								<option>Active</option>
								<option>Inactive</option>
							</select>
							<div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
								<svg
									className="fill-current h-4 w-4"
									xmlns="http://www.w3.org/2000/svg"
									viewBox="0 0 20 20"
								>
									<path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
								</svg>
							</div>
						</div>
					</div>
					<div className="block relative">
						<span className="h-full absolute inset-y-0 left-0 flex items-center pl-2">
							<svg
								viewBox="0 0 24 24"
								className="h-4 w-4 fill-current text-gray-500"
							>
								<path d="M10 4a6 6 0 100 12 6 6 0 000-12zm-8 6a8 8 0 1114.32 4.906l5.387 5.387a1 1 0 01-1.414 1.414l-5.387-5.387A8 8 0 012 10z"></path>
							</svg>
						</span>
						<input
							placeholder="Search"
							className="appearance-none rounded-r rounded-l sm:rounded-l-none border border-gray-400 border-b block pl-8 pr-6 py-2 w-full bg-white text-sm placeholder-gray-400 text-gray-700 focus:bg-white focus:placeholder-gray-600 focus:text-gray-700 focus:outline-none"
						/>
					</div>
				</div>
				<div className="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
					<div className="inline-block min-w-full shadow rounded-lg overflow-hidden">
						<table className="min-w-full leading-normal">
							<thead>
								<tr>
									<th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
										ID
									</th>
									<th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
										Title
									</th>
									<th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
										Company
									</th>
									<th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
										Location
									</th>
									<th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
										Position
									</th>
									<th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
										Salary
									</th>
									<th className="px-5 py-3 border-b-2 border-gray-200 bg-gray-100 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
										Action
									</th>
								</tr>
							</thead>
							<tbody>
								{/* {data?.map((item) => (
									<tr key={item.id}>
										<td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
											<div className="flex items-center">
												<div className="flex-shrink-0 w-10 h-10">{item.id}</div>
											</div>
										</td>
										<td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
											<p className="text-gray-900 whitespace-no-wrap">
												{item.title}
											</p>
										</td>
										<td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
											<p className="text-gray-900 whitespace-no-wrap">
												{item.company}
											</p>
										</td>
										<td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
											{item.locations?.map((location) => (
												<p
													key={location}
													className="text-gray-900 whitespace-no-wrap"
												>
													{location}
												</p>
											))}
										</td>
										<td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
											{item.positions?.map((position) => (
												<p
													key={position}
													className="text-gray-900 whitespace-no-wrap"
												>
													{position}
												</p>
											))}
										</td>
										<td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
											<span className="relative inline-block px-3 py-1 font-semibold text-green-900 leading-tight">
												<span
													aria-hidden
													className="absolute inset-0 bg-green-200 opacity-50 rounded-full"
												></span>
												<span className="relative">{item.salary}</span>
											</span>
										</td>
										<td className="px-5 py-5 border-b border-gray-200 bg-white text-sm">
											<button
												className="mr-10 bg-teal-500 hover:bg-teal-700 text-white font-bold py-2 px-4 rounded"
												onClick={() => navigate(`/detail/${item.id}`)}
											>
												<svg
													xmlns="http://www.w3.org/2000/svg"
													fill="none"
													viewBox="0 0 24 24"
													strokeWidth="1.5"
													stroke="currentColor"
													className="w-6 h-6"
												>
													<path
														strokeLinecap="round"
														strokeLinejoin="round"
														d="M11.25 11.25l.041-.02a.75.75 0 011.063.852l-.708 2.836a.75.75 0 001.063.853l.041-.021M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-9-3.75h.008v.008H12V8.25z"
													/>
												</svg>
											</button>
											<button
												className=" bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded"
												onClick={() => removeJob(item?.id)}
											>
												<svg
													xmlns="http://www.w3.org/2000/svg"
													fill="none"
													viewBox="0 0 24 24"
													strokeWidth="1.5"
													stroke="currentColor"
													className="w-6 h-6"
												>
													<path
														strokeLinecap="round"
														strokeLinejoin="round"
														d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0"
													/>
												</svg>
											</button>
										</td>
									</tr>
								))} */}
							</tbody>
						</table>
						<div className="px-5 py-5 bg-white border-t flex flex-col xs:flex-row items-center xs:justify-between          ">
							<span className="text-xs xs:text-sm text-gray-900">
								Showing 1 to 4 of 50 Entries
							</span>
							<div className="inline-flex mt-2 xs:mt-0">
								<button className="text-sm bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold py-2 px-4 rounded-l">
									Prev
								</button>
								<button className="text-sm bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold py-2 px-4 rounded-r">
									Next
								</button>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default AdminManageUser;
