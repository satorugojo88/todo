const isProduction = import.meta.env.PROD

const prod = "https://server-glowing-bird-431.fly.dev"
const dev = "http://localhost:5099"

 export const finalUrl = isProduction ? prod : dev

