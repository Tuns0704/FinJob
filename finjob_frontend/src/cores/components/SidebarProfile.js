import React from "react";
import { useContext } from "react";
import { authContext } from "../context/auth";

const SidebarProfile = () => {
	const {
		state: { user },
	} = useContext(authContext);
	return (
		<div className="max-w-sm py-10 px-10 flex flex-col gap-10">
			<div className="lg:mx-0 px-10 py-10 bg-white rounded-lg flex flex-col justify-center items-center gap-5">
				<img
					className="h-16 w-16 rounded-full"
					src="https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
					alt="userImage"
				/>
				<h2 className="text-2xl font-bold tracking-tight text-dark sm:text-2xl">
					{user.name}
				</h2>
				<p className="mt-2 text-lg leading-8 text-gray-400">
					Junior Front-End Developer
				</p>
				<button className="rounded-3xl bg-lightTurquoise py-2 px-10 w-60 text-sm font-semibold text-secondary shadow-sm">
					Edit Profile
				</button>
			</div>
			<div className="lg:mx-0 px-4 py-4 bg-white rounded-lg">
				<div className="flex justify-between align-middle">
					<h2 className="text-2xl font-bold tracking-tight text-primary sm:text-lg">
						Work Exprience
					</h2>
					<button
						className="rounded-full bg-lightTurquoise p-4 text-secondary font-bold"
						title="edit"
					>
						<svg
							xmlns="http://www.w3.org/2000/svg"
							fill="none"
							viewBox="0 0 24 24"
							strokeWidth={1.5}
							stroke="currentColor"
							className="w-5 h-5"
						>
							<path
								strokeLinecap="round"
								strokeLinejoin="round"
								d="M16.862 4.487l1.687-1.688a1.875 1.875 0 112.652 2.652L10.582 16.07a4.5 4.5 0 01-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 011.13-1.897l8.932-8.931zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0115.75 21H5.25A2.25 2.25 0 013 18.75V8.25A2.25 2.25 0 015.25 6H10"
							/>
						</svg>
					</button>
				</div>
			</div>
			<div className="lg:mx-0 px-10 py-10 bg-white rounded-lg flex flex-col justify-center items-center gap-5">
				<h2 className="text-lg font-bold tracking-tight text-primary sm:text-lg">
					Top Skill
				</h2>
			</div>
			<div className="lg:mx-0 px-10 py-10 bg-white rounded-lg flex flex-col justify-center items-center gap-5">
				<h2 className="text-2xl font-bold tracking-tight text-primary sm:text-2xl">
					Lastest Portfolio
				</h2>
			</div>
		</div>
	);
};

export default SidebarProfile;
