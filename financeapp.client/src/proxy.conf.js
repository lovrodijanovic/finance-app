const PROXY_CONFIG = [
  {
    context: [
      "/financialForm",
      "/user",
      "/account",
    ],
    target: "https://localhost:7008",
    secure: false,
    headers: {
      Connection: "Keep-Alive",
    },
  }
]

module.exports = PROXY_CONFIG;
