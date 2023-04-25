import { ErrorMessage } from "@hookform/error-message";
import Modal from "react-modal";
import { yupResolver } from "@hookform/resolvers/yup";
import { useContext } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import * as yup from "yup";
import { authContext } from "../../cores/context/auth";
import { applyJob } from "../../services/apply.service";
import { createConversation } from "../../services/chat.service";

const jobSchema = yup.object().shape({
	email: yup.string().email("Unvalid Email").required("Email is required"),
	phoneNumber: yup.string().required("Phone Number is required"),
	fullName: yup.string().required("Full Name is required"),
});

const ApplyModal = ({ isOpen, onClose, data, reload }) => {
	const {
		state: { token, user },
	} = useContext(authContext);

	const {
		formState: { errors, isValid },
		handleSubmit,
		register,
	} = useForm({
		resolver: yupResolver(jobSchema),
		defaultValues: {
			email: "",
			userId: user.id,
			positionId: "",
			phoneNumber: "",
			fullName: "",
			jobId: data.id,
		},
		mode: "all",
		reValidateMode: "onSubmit",
		criteriaMode: "all",
	});

	const onSubmit = async (body) => {
		try {
			const applyJobBody = {
				fullName: body.fullName,
				email: body.email,
				phoneNumber: body.phoneNumber,
				positionId: parseInt(body.positionId),
				jobId: data.id,
				userId: user.id,
			};
			const response = await applyJob(applyJobBody, token);
			if (response.status === 201) {
				toast.success("Apply success!!!");
				onClose();
				const conversation = {
					senderId: user.id,
					receiverId: data.userId,
				};
				const conversationResponse = await createConversation(
					conversation,
					token
				);
				if (conversationResponse.status === 201) {
					toast.success(
						"You have connect with Employer! You can direct in the messenger"
					);
				} else {
					toast.error("You already have the convertion with this employer!");
				}
				return;
			}
			toast.error("You have already apply for this job!!!");
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
						Full Name
						<input
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							type="text"
							name="fullName"
							{...register("fullName")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="fullName"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Email
						<input
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							type="email"
							name="email"
							{...register("email")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="email"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Phone Number
						<input
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							type="text"
							name="phoneNumber"
							{...register("phoneNumber")}
						/>
					</label>
					<ErrorMessage
						errors={errors}
						name="phoneNumber"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<label className="block text-gray-700 text-sm mb-2">
						Position
						<select
							className="border-0 px-3 py-3 placeholder-blueGray-300 text-blueGray-600 bg-white rounded text-sm shadow focus:outline-none focus:ring w-full ease-linear transition-all duration-150"
							name="positionId"
							{...register("positionId")}
						>
							{data.positionIds.map((positionId, index) => (
								<option key={positionId} value={positionId}>
									{data.positions[index]}
								</option>
							))}
						</select>
					</label>
					<ErrorMessage
						errors={errors}
						name="locationId"
						render={({ message }) => (
							<p className="text-primary text-sm">{message}</p>
						)}
					/>
					<div className="flex justify-center mt-10">
						<button
							disabled={!isValid}
							type="submit"
							className="bg-primary w-full text-white font-bold text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
						>
							Apply Now
						</button>
					</div>
				</form>
			</div>
		</Modal>
	);
};

export default ApplyModal;
