module.exports = {
  transform: {
    '.(js|jsx|ts|tsx)': '@sucrase/jest-plugin',
  },
  collectCoverageFrom: ['./src/**/*.js'],
  bail: true,
}
