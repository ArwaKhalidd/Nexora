export function formatK(n) {
  if (n >= 1_000_000) return `${(n / 1_000_000).toFixed(1)}M`;
  if (n >= 1_000) return `${(n / 1_000).toFixed(1)}K`;
  return n.toString();
}

export function formatPct(n, decimals = 1) {
  return `${n.toFixed(decimals)}%`;
}

export function formatScore(n) {
  return n.toFixed(1);
}