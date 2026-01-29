import { defineConfig } from 'vite';
import plugin from "@vitejs/plugin-react";

export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 3000,
        open: true,
    }
});