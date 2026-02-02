import type { MenuItem } from "../../types";

export const menuItems: MenuItem[] = [
  {
    label: "Home",
    url: "/",
  },
  {
    label: "Weather forecast",
    url: "/weather-forecasts",
  },
  {
    label: "Product",
    url: "/products",
  },
];

export const adminMenuItems: MenuItem[] = [
  {
    label: "Product admin",
    url: "/product-admin",
  },
];
