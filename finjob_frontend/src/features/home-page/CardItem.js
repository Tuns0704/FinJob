import { React, useState, useEffect, useCallback } from "react";
import { getCompany } from "../../services/company.service";
import { toast } from "react-toastify";

export default function CardItem(item) {
	const [company, setCompany] = useState([]);

	const getCompanyById = useCallback(async () => {
		try {
			const response = await getCompany(item.item.companyID);
			if (response.status === 200) {
				setCompany(response.data.result);
				console.log(response.data.result);
			}
		} catch (error) {
			console.log(error);
		}
	}, [item.item.companyID]);

	useEffect(() => {
		getCompanyById();
	}, [getCompanyById]);

	return (
		<div className="rounded-lg bg-white px-10 py-5 mb-5">
			<div className="flex justify-between mb-6">
				<div className="flex gap-2">
					<div className="w-16 h-16 p-4 flex justify-center bg-gray-100 rounded-lg">
						<img src={company.imageURL} alt="" />
					</div>
					<div className="flex flex-col">
						<div className="text-2xl font-medium">{item.item.title}</div>
						<div className="text-gray-400">{company.name}</div>
					</div>
				</div>
				<div>Header Button Card</div>
			</div>
			<div>
				<div className="text-justify mb-5">{item.item.description}</div>
				<div className="flex gap-2 mb-5">
					{item.item.locations.map((location) => (
						<div key={location} className="px-5 py-3 bg-gray-100 rounded-lg">
							{location}
						</div>
					))}
				</div>
			</div>
			<div className="flex justify-between">
				<div className="lg:flex sm:px-0 h-20 px-5 py-3 w-4/6 flex-row justify-between">
					<div className="flex gap-1 w-2/6">
						<div className="sm:flex">
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
						<div className="flex self-center text-secondary">
							{item.item.salary}
						</div>
					</div>
					<div className="flex gap-1 w-5/6">
						<div className="sm:flex">
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
									d="M18 18.72a9.094 9.094 0 003.741-.479 3 3 0 00-4.682-2.72m.94 3.198l.001.031c0 .225-.012.447-.037.666A11.944 11.944 0 0112 21c-2.17 0-4.207-.576-5.963-1.584A6.062 6.062 0 016 18.719m12 0a5.971 5.971 0 00-.941-3.197m0 0A5.995 5.995 0 0012 12.75a5.995 5.995 0 00-5.058 2.772m0 0a3 3 0 00-4.681 2.72 8.986 8.986 0 003.74.477m.94-3.197a5.971 5.971 0 00-.94 3.197M15 6.75a3 3 0 11-6 0 3 3 0 016 0zm6 3a2.25 2.25 0 11-4.5 0 2.25 2.25 0 014.5 0zm-13.5 0a2.25 2.25 0 11-4.5 0 2.25 2.25 0 014.5 0z"
								/>
							</svg>
						</div>
						<div className="flex self-center text-secondary">
							55 People Applied
						</div>
					</div>
				</div>
				<div className="flex items-center">
					<div className="py-3 rounded-lg bg-primary text-white text-bold">
						<div className="px-5 w-full flex justify-center self-center">
							Apply Now
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
