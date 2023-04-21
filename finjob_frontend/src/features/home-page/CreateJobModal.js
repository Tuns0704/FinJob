import { ErrorMessage } from "@hookform/error-message";
import Modal from "react-modal";
import { yupResolver } from "@hookform/resolvers/yup";
import { useContext, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import * as yup from "yup";
import { authContext } from "../../cores/context/auth";
import { createJob } from "../../services/job.service";
import { getAllLocations } from "../../services/location.service";
import Select from "react-select";
import { getAllPositions } from "../../services/position.service";

const jobSchema = yup.object().shape({
	title: yup.string().required("Title is required"),
	description: yup.string().required("Description is required"),
	positionIds: yup.array().of(yup.number()).required("Positions is required"),
	requirement: yup.string().required("Requirement is required"),
	locationIds: yup.array().of(yup.string()).required("Locations is required"),
	benefits: yup.string().required("Benifits is required"),
	salary: yup.string().required("Salary is required"),
});

const CreateJobModal = ({ isOpen, onClose }) => {
	const {
		state: { token, user },
	} = useContext(authContext);

	const [locationsFields, setLocationsFields] = useState([]);
	const [selectedLocations, setSelectedLocations] = useState([]);

	const getLocations = async () => {
		const { data: response } = await getAllLocations();
		if (response.statusCode === 200) {
			setLocationsFields(
				response.result.map((location) => ({
					value: parseInt(location.id),
					label: location.name,
				}))
			);
		}
	};

	useEffect(() => {
		getLocations();
	}, []);

	const handleChangeLocation = (selectedOptions) => {
		setSelectedLocations(selectedOptions);
	};

	const [positionsFields, setPositionsFields] = useState([]);
	const [selectedPositions, setSelectedPositions] = useState([]);

	const getPositions = async () => {
		const { data: response } = await getAllPositions();
		if (response.statusCode === 200) {
			setPositionsFields(
				response.result.map((position) => ({
					value: parseInt(position.id),
					label: position.name,
				}))
			);
		}
	};

	useEffect(() => {
		getPositions();
	}, []);

	const handleChangePosition = (selectedOptions) => {
		setSelectedPositions(selectedOptions);
	};

	const {
		formState: { errors, isValid },
		handleSubmit,
		register,
	} = useForm({
		resolver: yupResolver(jobSchema),
		defaultValues: {
			title: "",
			description: "",
			positionIds: [],
			locationIds: [],
			requirement: "",
			benefits: "",
			salary: "",
		},
		mode: "all",
		reValidateMode: "onSubmit",
		criteriaMode: "all",
	});

	const onSubmit = async (body) => {
		try {
			const job = {
				title: body.title,
				description: body.description,
				positionIds: selectedPositions.map((position) =>
					parseInt(position.value)
				),
				locationIds: selectedLocations.map((location) =>
					parseInt(location.value)
				),
				requirement: body.requirement,
				benefits: body.benefits,
				salary: body.salary,
				companyID: parseInt(user.companyId),
			};
			const response = await createJob(job, token);
			if (response.status === 201) {
				toast.success("Create Succeed");
				onClose();
				return;
			}
			toast.error("Create Failed");
		} catch (error) {
			console.log(error);
		}
	};

	return (
		<Modal
			className="flex justify-center items-center overflow-x-hidden overflow-y-auto fixed inset-0 z-50"
			style={{
				overlay: {
					backgroundColor: "rgba(0, 0, 0, 0.5)",
					zIndex: 1000,
				},
			}}
			isOpen={isOpen}
			onRequestClose={onClose}
			ariaHideApp={false}
		>
			<div className="bg-gray-100 sm:w-2/4 py-10 px-20 rounded-lg text-dark font-poppins">
				<button onClick={onClose}>
					<svg
						xmlns="http://www.w3.org/2000/svg"
						className="h-6 w-6 text-primary"
						fill="none"
						viewBox="0 0 24 24"
						stroke="currentColor"
					>
						<path
							strokeLinecap="round"
							strokeLinejoin="round"
							strokeWidth={2}
							d="M6 18L18 6M6 6l12 12"
						/>
					</svg>
				</button>

				<h2 className=" font-medium text-2xl text-center">Hiring Job</h2>
				<form onSubmit={handleSubmit(onSubmit)}>
					<label className="block text-gray-700 text-sm mb-2">
						Title
						<input
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							type="text"
							name="title"
							{...register("title")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="title"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Description
						<textarea
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							name="description"
							{...register("description")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="description"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Position
						<Select
							className="border-none placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							{...register("locationIds")}
							isMulti
							options={positionsFields}
							value={selectedPositions}
							onChange={handleChangePosition}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="locationIds"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Requirement
						<input
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							type="text"
							name="requirement"
							{...register("requirement")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="requirement"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Location
						<Select
							className="placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							placeholder="Select Company Location"
							{...register("locationIds")}
							isMulti
							options={locationsFields}
							value={selectedLocations}
							onChange={handleChangeLocation}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="locationIds"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Benefits
						<input
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							type="text"
							name="benefits"
							{...register("benefits")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="benefits"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Salary
						<input
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							name="salary"
							{...register("salary")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="salary"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<div className="flex justify-center mt-10">
						<button
							type="submit"
							className="bg-primary w-full text-white font-bold text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
						>
							Create
						</button>
					</div>
				</form>
			</div>
		</Modal>
	);
};

export default CreateJobModal;
