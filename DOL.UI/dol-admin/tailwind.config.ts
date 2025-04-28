/** @type {import('tailwindcss').Config} */
// const { createThemes } = require("tw-colors");

module.exports = {
  content: [
    "./node_modules/flowbite-react/**/*.js",
    "./node_modules/flowbite/**/*.js",
    "./node_modules/react-tailwindcss-select/dist/index.esm.js",
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        "dashboard-red": "#D92450",
        "dashboard-indigo": "#666DDC",
        "dashboard-yellow": "#F7BA25",
        "dashboard-green": "#3AC47D",
      },
      // backgroundImage: {
      //   "gradient-radial": "radial-gradient(var(--tw-gradient-stops))",
      //   "gradient-conic":
      //     "conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))",
      // },
    },
    // backgroundColor: (theme) => ({
    //   //get the value from the color definitions above (7th line from top)
    //   light: theme("colors.red"),
    //   dark: {
    //     DEFAULT: theme("colors.dark.DEFAULT"),
    //     dark: theme("colors.dark.dark"),
    //   },
    // }),
  },
  plugins: [
    require("flowbite/plugin"),
    // createThemes(({ light, dark }) => ({
    //   light: light({
    //     base: "#FFFFFF",
    //     "on-base": "#3F3D3B",
    //     "disable-base": "#e5e7eb",

    //     primary: "#38A3A5",
    //     "on-primary": "#FFFFFF",

    //     secondary: "#22577A",
    //     "on-secondary": "#FFFFFF",

    //     tertiary: "#106EBE",
    //     "on-tertiary": "#FFFFFF",

    //     accent: "#57CC99",
    //     "on-accent": "#FFFFFF",

    //     neutral: "#999797",
    //     "on-neutral": "#1E1F21",

    //     "neutral-1": "#C4C4C4",
    //     "on-neutral-1": "#111827",
    //     "hover-neutral-1": "#8D8D8D",
    //     "ring-neutral-1": "#C4C4C4",

    //     hilight: "#E02D3C",
    //     "on-hilight": "#FFFFFF",
    //     "hover-hilight": "#BA2532",
    //     "ring-hilight": "#E02D3C",

    //     surface: "#F4F4F4",
    //     "on-surface": "#616161",

    //     "surface-2": "#FFFFFF",
    //     "on-surface-2": "#111827",
    //     "hover-surface-2": "#e5e7eb",

    //     "surface-3": "#f9fafb",
    //     "on-surface-3": "#111827",
    //     "hover-surface-3": "#e5e7eb",

    //     info: "#EEF0F3",
    //     "on-info": "#22577A",

    //     success: "#57CC99",
    //     "on-success": "#FFFFFF",
    //     "hover-success": "#51BB8D",
    //     "ring-success": "#57CC99",

    //     warning: "#ECD468",
    //     "on-warning": "#FFFFFF",
    //     "hover-warning": "#DEB950",
    //     "ring-warning": "#ECD468",

    //     error: "#F24E1E",
    //     "on-error": "#FFFFFF",

    //   }),
    //   dark: dark({
    //     base: "#121212",
    //     "on-base": "#FFFFFF",
    //     "disable-base": "#1E1F21",

    //     primary: "#38A3A5",
    //     "on-primary": "#FFFFFF",

    //     secondary: "#7075f2",
    //     "on-secondary": "#FFFFFF",

    //     tertiary: "#106EBE",
    //     "on-tertiary": "#FFFFFF",

    //     accent: "#57CC99",
    //     "on-accent": "#FFFFFF",

    //     neutral: "#B3B3B3",
    //     "on-neutral": "#1E1F21",

    //     "neutral-1": "#C4C4C4",
    //     "on-neutral-1": "#111827",
    //     "hover-neutral-1": "#8D8D8D",
    //     "ring-neutral-1": "#C4C4C4",

    //     hilight: "#E02D3C",
    //     "on-hilight": "#FFFFFF",
    //     "hover-hilight": "#BA2532",
    //     "ring-hilight": "#E02D3C",

    //     surface: "#080808",
    //     "on-surface": "#FFFFFF",

    //     "surface-2": "#505050",
    //     "on-surface-2": "#FFFFFF",
    //     "hover-surface-2": "#080808",

    //     "surface-3": "#505050",
    //     "on-surface-3": "#FFFFFF",
    //     "hover-surface-3": "#080808",

    //     info: "#EEF0F3",
    //     "on-info": "#22577A",

    //     success: "#57CC99",
    //     "on-success": "#FFFFFF",
    //     "hover-success": "#51BB8D",
    //     "ring-success": "#57CC99",

    //     warning: "#ECD468",
    //     "on-warning": "#FFFFFF",
    //     "hover-warning": "#DEB950",
    //     "ring-warning": "#ECD468",

    //     error: "#F24E1E",
    //     "on-error": "#FFFFFF",

    //   }),
    // })),
  ],
  // corePlugins: {
  //   preflight: false // <== disable this!
  // },
  //darkMode: ["class", '[data-mode="dark"]'],
};
