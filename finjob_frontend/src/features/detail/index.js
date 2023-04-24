import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getJob } from "../../services/job.service";
import { getCompany } from "../../services/company.service";
import { toast } from "react-toastify";

const JobDetail = () => {
	const { id } = useParams();
	const navigate = useNavigate();

	const [job, setJob] = useState({});
	const [company, setCompany] = useState({});

	const getPostById = useCallback(async () => {
		try {
			const response = await getJob(id);
			if (response.data.statusCode === 200) {
				setJob(response.data.result);
				try {
					const companyResponse = await getCompany(
						response.data.result.companyID
					);
					if (companyResponse.status === 200) {
						setCompany(companyResponse.data.result);
					}
				} catch {
					toast.error("Error while getting company");
				}
			}
		} catch (error) {
			console.log(error);
		}
	}, [id]);

	useEffect(() => {
		getPostById();
	}, [getPostById]);

	return (
		<div className="flex justify-center mt-10">
			<div className="row">
				<div className="col-md-8">
					<div className="block rounded-lg bg-white p-10 shadow-[0_2px_15px_-3px_rgba(0,0,0,0.07),0_10px_20px_-2px_rgba(0,0,0,0.04)]">
						<div className="flex flex-row flex-column">
							<div className="w-16 h-16 p-4 flex justify-center bg-gray-100 rounded-lg">
								<img src={company.imageURL} alt="" />
							</div>
							<div className="pl-5 flex flex-col">
								<div className="text-2xl font-medium">{job.title}</div>
								<div className="text-gray-500">{company.name}</div>
							</div>
						</div>
						<div className="pt-5">
							<p className="flex max-w-3xl text-justify">{job.description}</p>
						</div>
						<div className="flex mt-5">
							<div className="flex items-center">
								<div>
									<svg
										xmlns="http://www.w3.org/2000/svg"
										fill="none"
										viewBox="0 0 24 24"
										strokeWidth="1.5"
										stroke="currentColor"
										className="w-7 text-secondary"
									>
										<path
											strokeLinecap="round"
											strokeLinejoin="round"
											d="M12 6v12m-3-2.818l.879.659c1.171.879 3.07.879 4.242 0 1.172-.879 1.172-2.303 0-3.182C13.536 12.219 12.768 12 12 12c-.725 0-1.45-.22-2.003-.659-1.106-.879-1.106-2.303 0-3.182s2.9-.879 4.006 0l.415.33M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
										/>
									</svg>
								</div>
								<div className="ml-2 text-gray-500 text-center self-center">
									Salary:{" "}
									<p className="text-gray-900 inline-block ml-5 whitespace-no-wrap bg-light px-5 py-2 rounded-lg">
										{job.salary}
									</p>
								</div>
							</div>
						</div>
						<div className="flex mt-5">
							<div className="flex items-center">
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth="1.5"
									stroke="currentColor"
									className="w-6 h-6 text-secondary"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M9 12h3.75M9 15h3.75M9 18h3.75m3 .75H18a2.25 2.25 0 002.25-2.25V6.108c0-1.135-.845-2.098-1.976-2.192a48.424 48.424 0 00-1.123-.08m-5.801 0c-.065.21-.1.433-.1.664 0 .414.336.75.75.75h4.5a.75.75 0 00.75-.75 2.25 2.25 0 00-.1-.664m-5.8 0A2.251 2.251 0 0113.5 2.25H15c1.012 0 1.867.668 2.15 1.586m-5.8 0c-.376.023-.75.05-1.124.08C9.095 4.01 8.25 4.973 8.25 6.108V8.25m0 0H4.875c-.621 0-1.125.504-1.125 1.125v11.25c0 .621.504 1.125 1.125 1.125h9.75c.621 0 1.125-.504 1.125-1.125V9.375c0-.621-.504-1.125-1.125-1.125H8.25zM6.75 12h.008v.008H6.75V12zm0 3h.008v.008H6.75V15zm0 3h.008v.008H6.75V18z"
									/>
								</svg>
								<div className="ml-2 text-gray-500 text-center self-center">
									Requirement:
									<p className="text-gray-900 inline-block ml-5 whitespace-no-wrap bg-light px-5 py-2 rounded-lg">
										{job.requirement}
									</p>
								</div>
							</div>
						</div>
						<div className="flex flex-wrap mt-5">
							<div className="flex items-center">
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth="1.5"
									stroke="currentColor"
									className="w-6 h-6 text-secondary"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M15 10.5a3 3 0 11-6 0 3 3 0 016 0z"
									/>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M19.5 10.5c0 7.142-7.5 11.25-7.5 11.25S4.5 17.642 4.5 10.5a7.5 7.5 0 1115 0z"
									/>
								</svg>
								<div className="ml-2 text-gray-500 text-center self-center">
									Location:
									{job.locations?.map((location) => (
										<p
											key={location}
											className="text-gray-900 inline-block ml-5 whitespace-no-wrap bg-light px-5 py-2 rounded-lg"
										>
											{location}
										</p>
									))}
								</div>
							</div>
						</div>
						<div className="flex flex-wrap mt-5">
							<div className="flex items-center">
								<svg
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 24 24"
									strokeWidth="1.5"
									stroke="currentColor"
									className="w-6 h-6 text-secondary"
								>
									<path
										strokeLinecap="round"
										strokeLinejoin="round"
										d="M20.25 14.15v4.25c0 1.094-.787 2.036-1.872 2.18-2.087.277-4.216.42-6.378.42s-4.291-.143-6.378-.42c-1.085-.144-1.872-1.086-1.872-2.18v-4.25m16.5 0a2.18 2.18 0 00.75-1.661V8.706c0-1.081-.768-2.015-1.837-2.175a48.114 48.114 0 00-3.413-.387m4.5 8.006c-.194.165-.42.295-.673.38A23.978 23.978 0 0112 15.75c-2.648 0-5.195-.429-7.577-1.22a2.016 2.016 0 01-.673-.38m0 0A2.18 2.18 0 013 12.489V8.706c0-1.081.768-2.015 1.837-2.175a48.111 48.111 0 013.413-.387m7.5 0V5.25A2.25 2.25 0 0013.5 3h-3a2.25 2.25 0 00-2.25 2.25v.894m7.5 0a48.667 48.667 0 00-7.5 0M12 12.75h.008v.008H12v-.008z"
									/>
								</svg>

								<div className="ml-2 text-gray-500 text-center self-center">
									Position:
									{job.positions?.map((position) => (
										<p
											key={position}
											className="text-gray-900 inline-block ml-5 whitespace-no-wrap bg-light px-5 py-2 rounded-lg"
										>
											{position}
										</p>
									))}
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default JobDetail;
