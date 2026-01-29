import { createBrowserRouter } from "react-router";
import App from "../../App";
import { LoginForm } from "../LoginForm/LoginForm";

export const router = createBrowserRouter([
    {
        element: <App />,
        path: "/",
    },
    {
        element: <LoginForm />,
        path: "/login",
    },
]);