import { useRestApi } from "../../hooks/useRestApi";
import type { ProductDto } from "../../types";
import { ProductList } from "./ProductList";

export function Products() {
  const { items } = useRestApi<ProductDto>("/product");

  return <ProductList products={items ?? []} />;
}
