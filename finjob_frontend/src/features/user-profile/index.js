import React from "react";
import { useCallback, useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { authContext } from "../../cores/context/auth";
import { getUserById } from "../../services/user.service";

const UserProfile = () => {
	const {
		state: { token, user },
	} = useContext(authContext);
	const { id } = useParams();
	const navigate = useNavigate();

	const [profile, setProfile] = useState([]);

	const getProfile = useCallback(async () => {
		try {
			const response = await getUserById(id, token);
			if (response.data.statusCode === 200) {
				setProfile(response.data.result);
			} else {
				navigate("/404");
			}
		} catch (error) {
			console.log(error);
		}
	}, [id, token]);

	useEffect(() => {
		getProfile();
	}, [getProfile]);

	return (
		<div>
			<div className="lg:px-24 lg:py-24 md:py-20 md:px-44 px-4 py-24 items-center flex justify-center flex-col-reverse lg:flex-row md:gap-28 gap-16">
				{profile.userName}
				<div className="flex flex-col-reverse">
					<div className="flex flex-col gap-4">
						<div className="flex flex-col gap-2">
							<h1 className="text-4xl font-bold text-gray-800">
								{profile.userName}
							</h1>
							<img src={profile.avatar} alt="" />
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default UserProfile;
