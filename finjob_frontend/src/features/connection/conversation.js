import { useContext, useEffect, useState, useCallback } from "react";
import { authContext } from "../../cores/context/auth";
import { getUserById } from "../../services/user.service";

const Conversation = ({ conversation }) => {
	const {
		state: { user, token },
	} = useContext(authContext);

	const [userSenderProfile, setUserSenderProfile] = useState([]);

	const getUserSenderProfile = useCallback(async () => {
		try {
			let otherUserId =
				conversation.senderId === user.id
					? conversation.receiverId
					: conversation.senderId;
			const response = await getUserById(otherUserId, token);
			if (response.data.statusCode === 200) {
				setUserSenderProfile(response.data.result);
			} else {
				console.log("User profile is null");
			}
		} catch (error) {
			console.log(error);
		}
	}, [conversation.receiverId, conversation.senderId, token, user.id]);

	useEffect(() => {
		getUserSenderProfile();
	}, [getUserSenderProfile]);

	return (
		<div className="flex p-5 border-b-2 hover:cursor-pointer">
			<div className="lg:mr-5">
				<img
					className="rounded-full w-16 h-16"
					src={
						userSenderProfile.avatar === null
							? "https://icon-library.com/images/default-user-icon/default-user-icon-4.jpg"
							: userSenderProfile.avatar
					}
					alt=""
				/>
			</div>
			<div className="hidden lg:flex flex-col justify-center">
				<div className="text-lg font-semibold">{userSenderProfile.name}</div>
			</div>
		</div>
	);
};

export default Conversation;
