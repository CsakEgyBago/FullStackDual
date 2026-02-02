import { createBrowserRouter } from "react-router";
import App from "../../App";
import { LoginForm } from "../LoginForm/LoginForm";
import { Layout } from "../Layout/Layout";
import { AuthenticatedLayout } from "../Layout/AuthenticatedLayout";
import { AdminLayout } from "../Layout/AdminLayout";
import { Products } from "../Product/Products";

export const router = createBrowserRouter([
  {
    element: <Layout />,
    children: [
      {
        element: <div>Home</div>,
        path: "/",
      },

      {
        element: <LoginForm />,
        path: "/login",
      },
      {
        element: <Products />,
        path: "/products",
      },
      {
        element: <AuthenticatedLayout />,
        children: [
          {
            element: <App />,
            path: "/weather-forecasts",
          },
        ],
      },
      {
        element: <AdminLayout />,
        children: [
          {
            element: <div>Product admin</div>,
            path: "/product-admin",
          },
        ],
      },
    ],
  },
]);
