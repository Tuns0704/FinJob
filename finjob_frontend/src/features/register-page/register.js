import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { ErrorMessage } from "@hookform/error-message";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { register as registerAccount } from "../../services/auth.service";
import { getAllLocations } from "../../services/location.service";
import { createCompany } from "../../services/company.service";
import logo from "../../assets/FinJobLogo.png";
import { useEffect, useState } from "react";
import Select from "react-select";

const validationSchema = yup.object().shape({
	role: yup
		.string("Employee" | "BusinessEmployer")
		.required("You Must Choose an Account Type"),
	name: yup
		.string()
		// .email("Email is invalid")
		.required("Name must be filled"),
	userName: yup.string().required("Username is required"),
	password: yup
		.string()
		.required("Please enter a password")
		.matches(
			/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
			"Must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character"
		),
	company: yup.object().shape({
		name: yup.string().required("Company name is required"),
		description: yup.string().required("Company description is required"),
		scale: yup.string().required("Company scale is required"),
		location: yup.array().required("Company location is required"),
		imageURL: yup.string().required("Company image URL is required"),
	}),
	confirmPassword: yup
		.string()
		.oneOf([yup.ref("password"), null], "Passwords must match")
		.required("Confirm password is required"),
	terms: yup
		.boolean()
		.oneOf([true], "You must accept the terms and conditions"),
});

export default function RegisterPage() {
	const navigate = useNavigate();
	const {
		register,
		handleSubmit,
		formState: { errors, isValid },
		watch,
	} = useForm({
		resolver: yupResolver(validationSchema),
		defaultValues: {
			role: "",
			name: "",
			userName: "",
			password: "",
			confirmPassword: "",
			terms: false,
			company: {
				name: "",
				description: "",
				scale: "",
				location: [],
				imageURL: "",
			},
		},
		mode: "all",
		reValidateMode: "onSubmit",
		criteriaMode: "all",
	});

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

	const handleChange = (selectedOptions) => {
		setSelectedLocations(selectedOptions);
		console.log(selectedLocations);
	};

	const onSubmit = async (body) => {
		try {
			let response;
			if (body.role === "BusinessEmployer") {
				const company = {
					name: body.company.name,
					description: body.company.description,
					scale: body.company.scale,
					locationIds: selectedLocations.map((location) =>
						parseInt(location.value)
					),
					imageURL: body.company.imageURL,
				};
				console.log(company.locationIds);
				const { data: companyResponse } = await createCompany(company);

				if (companyResponse.statusCode === 201) {
					const user = {
						role: body.role,
						name: body.name,
						userName: body.userName,
						password: body.password,
						companyId: companyResponse.result.id,
					};
					({ data: response } = await registerAccount(user));
				}
			} else if (body.role === "Employee") {
				({ data: response } = await registerAccount(body));
			}

			if (response.statusCode === 200) {
				toast.success("Register success!");
				navigate("/login");
			} else {
				toast.error(response.errorMessages[0]);
			}
		} catch (error) {
			console.error(error);
			toast.error("An error occurred. Please try again.");
		}
	};

	return (
		<div className="gradient pt-10 min-h-screen">
			<div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-fit lg:py-0">
				<div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 ">
					<div className="p-6 space-y-4 md:space-y-6 sm:p-8">
						<Link
							to="/"
							className="flex w-full justify-center text-2xl font-semibold text-dark"
						>
							<img className="w-40" src={logo} alt="logo" />
						</Link>
						<h1 className="text-xl font-bold leading-tight tracking-tight md:text-2xl text-primary">
							Create and account
						</h1>
						<form
							className="space-y-4 md:space-y-6"
							onSubmit={handleSubmit(onSubmit)}
						>
							<div>
								<label className="block mb-2 text-sm font-medium text-gray-90">
									Username
									<input
										type="text"
										className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
										placeholder="Username"
										{...register("userName")}
									/>
								</label>
								<ErrorMessage
									errors={errors}
									name="userName"
									render={({ message }) => (
										<p className="text-primary text-sm">{message}</p>
									)}
								/>
							</div>
							<div>
								<label className="block mb-2 text-sm font-medium text-gray-90">
									Your Name
									<input
										type="text"
										className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
										placeholder="Nguyen Van A"
										{...register("name")}
									/>
								</label>
								<ErrorMessage
									errors={errors}
									name="name"
									render={({ message }) => (
										<p className="text-primary text-sm">{message}</p>
									)}
								/>
							</div>
							<div>
								<label className="block mb-2 text-sm font-medium text-dark">
									Password
									<input
										placeholder="••••••••"
										className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
										type="password"
										{...register("password")}
									/>
								</label>
								<ErrorMessage
									errors={errors}
									name="password"
									render={({ message }) => (
										<p className="text-primary text-sm">{message}</p>
									)}
								/>
							</div>
							<div>
								<label className="block mb-2 text-sm font-medium text-dark">
									Confirm password
									<input
										placeholder="••••••••"
										type="password"
										{...register("confirmPassword")}
										className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
									/>
								</label>
								<ErrorMessage
									errors={errors}
									name="confirmPassword"
									render={({ message }) => (
										<p className="text-primary text-sm">{message}</p>
									)}
								/>
							</div>
							<div>
								<label>
									You are?
									<select
										className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
										{...register("role")}
									>
										<option value="">Select</option>
										<option value="Employee">Employee</option>
										<option value="BusinessEmployer">Business Employer</option>
									</select>
								</label>
								<ErrorMessage
									errors={errors}
									name="role"
									render={({ message }) => (
										<p className="text-primary text-sm">{message}</p>
									)}
								/>
							</div>
							{watch("role") === "BusinessEmployer" && (
								<>
									<div>
										<label className="block mb-2 text-sm font-medium text-gray-90">
											Company Name
											<input
												type="text"
												className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
												placeholder="FinJob Technology"
												{...register("company.name")}
											/>
										</label>
										<ErrorMessage
											errors={errors}
											name="company.name"
											render={({ message }) => (
												<p className="text-primary text-sm">{message}</p>
											)}
										/>
									</div>
									<div>
										<label className="block mb-2 text-sm font-medium text-gray-90">
											Company Description
											<input
												type="text"
												className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
												placeholder="FinJob is a high technology company..."
												{...register("company.description")}
											/>
										</label>
										<ErrorMessage
											errors={errors}
											name="company.description"
											render={({ message }) => (
												<p className="text-primary text-sm">{message}</p>
											)}
										/>
									</div>
									<div>
										<label className="block mb-2 text-sm font-medium text-gray-90">
											Company Scale
											<input
												type="text"
												className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
												placeholder="FinJob Technology"
												{...register("company.scale")}
											/>
										</label>
										<ErrorMessage
											errors={errors}
											name="company.scale"
											render={({ message }) => (
												<p className="text-primary text-sm">{message}</p>
											)}
										/>
									</div>
									<Select
										className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
										placeholder="Select Company Location"
										{...register("company.location")}
										isMulti
										options={locationsFields}
										value={selectedLocations}
										onChange={handleChange}
									/>
									<ErrorMessage
										errors={errors}
										name="company.location"
										render={({ message }) => (
											<p className="text-primary text-sm">{message}</p>
										)}
									/>
									<div>
										<label
											htmlFor="companyImageURL"
											className="block mb-2 text-sm font-medium text-gray-90"
										>
											Company Image URL
										</label>
										<input
											type="text"
											name="companyImageURL"
											id="companyImageURL"
											className="bg-light border-lightOrange text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5"
											placeholder="FinJob Technology"
											{...register("company.imageURL")}
										/>
										<ErrorMessage
											errors={errors}
											name="company.imageURL"
											render={({ message }) => (
												<p className="text-primary text-sm">{message}</p>
											)}
										/>
									</div>
								</>
							)}
							<div className="flex items-start">
								<div className="flex items-center h-5">
									<input
										id="terms"
										aria-describedby="terms"
										type="checkbox"
										className="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-primary-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-primary-600 dark:ring-offset-gray-800"
										{...register("terms")}
									/>
								</div>
								<div className="ml-3 text-sm">
									<label htmlFor="terms" className="font-light text-dark">
										I accept the{" "}
										<Link
											className="font-medium text-dark hover:underline"
											to="/"
										>
											Terms and Conditions
										</Link>
									</label>
								</div>
								<ErrorMessage
									errors={errors}
									name="terms"
									render={({ message }) => (
										<p className="text-primary text-sm">{message}</p>
									)}
								/>
							</div>
							<button
								type="submit"
								className="w-full text-white bg-primary hover:bg-primary focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800"
								disabled={!isValid}
							>
								Create an account
							</button>
							<p className="text-sm font-light text-gray-500 dark:text-gray-400">
								Already have an account?{" "}
								<Link
									to="/login"
									className="font-medium text-primary hover:underline dark:text-primary-500"
								>
									Login here
								</Link>
							</p>
						</form>
					</div>
				</div>
			</div>
			<div className="relative -mt-12 lg:-mt-24">
				<svg viewBox="0 0 1428 174" xmlns="http://www.w3.org/2000/svg">
					<g stroke="none" strokeWidth="1" fill="none" fillRule="evenodd">
						<g
							transform="translate(-2.000000, 44.000000)"
							fill="#FFFFFF"
							fillRule="nonzero"
						>
							<path
								d="M0,0 C90.7283404,0.927527913 147.912752,27.187927 291.910178,59.9119003 C387.908462,81.7278826 543.605069,89.334785 759,82.7326078 C469.336065,156.254352 216.336065,153.6679 0,74.9732496"
								opacity="0.100000001"
							></path>
							<path
								d="M100,104.708498 C277.413333,72.2345949 426.147877,52.5246657 546.203633,45.5787101 C666.259389,38.6327546 810.524845,41.7979068 979,55.0741668 C931.069965,56.122511 810.303266,74.8455141 616.699903,111.243176 C423.096539,147.640838 250.863238,145.462612 100,104.708498 Z"
								opacity="0.100000001"
							></path>
							<path
								d="M1046,51.6521276 C1130.83045,29.328812 1279.08318,17.607883 1439,40.1656806 L1439,120 C1271.17211,77.9435312 1140.17211,55.1609071 1046,51.6521276 Z"
								id="Path-4"
								opacity="0.200000003"
							></path>
						</g>
						<g
							transform="translate(-4.000000, 76.000000)"
							fill="#FFFFFF"
							fillRule="nonzero"
						>
							<path d="M0.457,34.035 C57.086,53.198 98.208,65.809 123.822,71.865 C181.454,85.495 234.295,90.29 272.033,93.459 C311.355,96.759 396.635,95.801 461.025,91.663 C486.76,90.01 518.727,86.372 556.926,80.752 C595.747,74.596 622.372,70.008 636.799,66.991 C663.913,61.324 712.501,49.503 727.605,46.128 C780.47,34.317 818.839,22.532 856.324,15.904 C922.689,4.169 955.676,2.522 1011.185,0.432 C1060.705,1.477 1097.39,3.129 1121.236,5.387 C1161.703,9.219 1208.621,17.821 1235.4,22.304 C1285.855,30.748 1354.351,47.432 1440.886,72.354 L1441.191,104.352 L1.121,104.031 L0.457,34.035 Z"></path>
						</g>
					</g>
				</svg>
			</div>
		</div>
	);
}
