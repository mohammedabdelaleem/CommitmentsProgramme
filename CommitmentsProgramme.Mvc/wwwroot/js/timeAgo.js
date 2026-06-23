function timeAgo(dateString) {
  const now = new Date();
  const past = new Date(dateString);

  const diff = Math.floor((now - past) / 1000); // seconds

  if (diff < 10) return "just now";

  if (diff < 60) return `${diff} seconds ago`;

  const minutes = Math.floor(diff / 60);
  if (minutes < 60) return `${minutes} minute${minutes > 1 ? "s" : ""} ago`;

  const hours = Math.floor(minutes / 60);
  if (hours < 24) return `${hours} hour${hours > 1 ? "s" : ""} ago`;

  const days = Math.floor(hours / 24);
  if (days < 7) return `${days} day${days > 1 ? "s" : ""} ago`;

  const weeks = Math.floor(days / 7);
  if (weeks < 4) return `${weeks} week${weeks > 1 ? "s" : ""} ago`;

  const months = Math.floor(days / 30);
  if (months < 12) return `${months} month${months > 1 ? "s" : ""} ago`;

  const years = Math.floor(days / 365);
  return `${years} year${years > 1 ? "s" : ""} ago`;
}

// for initial 
document.querySelectorAll(".post-time").forEach(el => {
  const date = el.dataset.time; //  cleaner than getAttribute
  el.textContent = timeAgo(date);
});

setInterval(() => {
  document.querySelectorAll(".post-time").forEach(el => {
    el.textContent = timeAgo(el.dataset.time);
  });
}, 3000);