import { useContext, useEffect, useState, useCallback } from "react";
import { authContext } from "../../cores/context/auth";
import { getUserById } from "../../services/user.service";

const MessageText = ({ message, own, currentChat }) => {
	const {
		state: { user, token },
	} = useContext(authContext);

	const [userSenderProfile, setUserSenderProfile] = useState([]);
	const [userCurrent, setUserCurrent] = useState([]);

	const getUserSenderProfile = useCallback(async () => {
		try {
			let otherUserId =
				currentChat.senderId === user.id
					? currentChat.receiverId
					: currentChat.senderId;
			const response = await getUserById(otherUserId, token);
			if (response.data.statusCode === 200) {
				setUserSenderProfile(response.data.result);
			} else {
				console.log("User profile is null");
			}
		} catch (error) {
			console.log(error);
		}
	}, [currentChat.receiverId, currentChat.senderId, token, user.id]);
	const getCurrentUser = useCallback(async () => {
		try {
			const response = await getUserById(user.id, token);
			if (response.data.statusCode === 200) {
				setUserCurrent(response.data.result);
			} else {
				console.log("User profile is null");
			}
		} catch (error) {
			console.log(error);
		}
	}, [token, user.id]);

	useEffect(() => {
		getUserSenderProfile();
	}, [getUserSenderProfile]);

	useEffect(() => {
		getCurrentUser();
	}, [getCurrentUser]);

	return (
		<div>
			{own ? (
				<div className="flex justify-between items-center flex-row-reverse p-5">
					<div className="flex flex-row items-center">
						<div className="flex flex-col justify-between">
							<span className="text-light bg-primary p-5 rounded-l-lg rounded-tr-lg">
								{message.text}
							</span>
						</div>
						<div className="ml-5">
							<img
								className="rounded-full w-16 h-16"
								src={
									userCurrent.avatar === null
										? "https://icon-library.com/images/default-user-icon/default-user-icon-4.jpg"
										: userCurrent.avatar
								}
								alt="userCurrentAvatar"
							/>
						</div>
					</div>
				</div>
			) : (
				<div className="flex justify-between items-center p-5">
					<div className="flex flex-row items-center">
						<div className="mr-5">
							<img
								className="rounded-full w-16 h-16"
								src={
									userSenderProfile.avatar === null
										? "https://icon-library.com/images/default-user-icon/default-user-icon-4.jpg"
										: userSenderProfile.avatar
								}
								alt="userCurrentAvatar"
							/>
						</div>
						<div className="flex flex-col justify-between">
							<span className="text-light bg-primary p-5 rounded-r-lg rounded-tl-lg">
								{message.text}
							</span>
						</div>
					</div>
				</div>
			)}
		</div>
	);
};

export default MessageText;
