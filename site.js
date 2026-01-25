// BankingPro - Professional Banking JavaScript
document.addEventListener('DOMContentLoaded', function () {

    console.log('🏦 BankingPro JS Loaded Successfully! 🚀');

    // 1. EMI Calculator
    const emiBtn = document.getElementById('emiCalculate');
    if (emiBtn) {
        emiBtn.addEventListener('click', function (e) {
            e.preventDefault();
            const principal = parseFloat(document.getElementById('principal')?.value || 0);
            const rate = parseFloat(document.getElementById('rate')?.value || 0);
            const tenure = parseFloat(document.getElementById('tenure')?.value || 0);

            if (principal && rate && tenure) {
                const monthlyRate = rate / (12 * 100);
                const emi = (principal * monthlyRate * Math.pow(1 + monthlyRate, tenure)) /
                    (Math.pow(1 + monthlyRate, tenure) - 1);
                document.getElementById('emiResult').innerText = `₹${emi.toFixed(2)}/month`;
                document.getElementById('emiResult').style.display = 'block';
            }
        });
    }

    // 2. Quick Transfer Form (Real-time validation)
    const transferForm = document.querySelector('form[action*="TransferMoney"]');
    if (transferForm) {
        transferForm.addEventListener('submit', function (e) {
            const amount = parseFloat(document.querySelector('input[name="amount"]')?.value || 0);
            if (amount > 100000) {
                e.preventDefault();
                alert('❌ Maximum transfer limit: ₹1,00,000');
                return false;
            }
        });

        // Real-time balance check
        document.querySelector('input[name="amount"]').addEventListener('input', function () {
            const amount = parseFloat(this.value || 0);
            const balanceEl = document.querySelector('.balance-display');
            if (balanceEl) {
                const balance = parseFloat(balanceEl.textContent.replace(/[^\d.]/g, '') || 0);
                const remainingEl = document.getElementById('remainingBalance');
                if (remainingEl) {
                    remainingEl.textContent = `Remaining: ₹${(balance - amount).toFixed(2)}`;
                    remainingEl.style.color = amount > balance ? '#dc3545' : '#10b981';
                }
            }
        });
    }

    // 3. Navbar Smooth Scroll & Active Link
    document.querySelectorAll('.navbar-nav a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });

    // 4. Feature Cards Hover Animation
    document.querySelectorAll('.feature-card').forEach(card => {
        card.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-15px) scale(1.02)';
        });
        card.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });

    // 5. Balance Animation (Dashboard)
    const balanceEl = document.querySelector('.balance-display');
    if (balanceEl) {
        const balance = parseFloat(balanceEl.textContent.replace(/[^\d.]/g, ''));
        const animateBalance = (start, end, duration = 2000) => {
            let startTime = null;
            const step = (timestamp) => {
                if (!startTime) startTime = timestamp;
                const progress = Math.min((timestamp - startTime) / duration, 1);
                balanceEl.textContent = `₹${(start + (end - start) * progress).toLocaleString('en-IN', { maximumFractionDigits: 2 })}`;
                if (progress < 1) requestAnimationFrame(step);
            };
            requestAnimationFrame(step);
        };
        animateBalance(0, balance);
    }

    // 6. Live Chat Toggle
    document.querySelector('.chat-toggle')?.addEventListener('click', function () {
        document.getElementById('liveChat').classList.toggle('d-none');
    });

    // 7. Dark Mode Toggle
    document.getElementById('darkModeToggle')?.addEventListener('click', function () {
        document.body.classList.toggle('dark-mode');
        localStorage.setItem('darkMode', document.body.classList.contains('dark-mode'));
    });

    // 8. Real-time Clock (Dashboard)
    function updateClock() {
        const now = new Date();
        document.getElementById('liveClock') && (document.getElementById('liveClock').textContent =
            now.toLocaleString('en-IN', {
                timeZone: 'Asia/Kolkata',
                hour12: true
            }));
    }
    setInterval(updateClock, 1000);
    updateClock();

    // 9. Transaction Filter (Dashboard)
    document.querySelectorAll('.transaction-filter')?.forEach(filter => {
        filter.addEventListener('change', function () {
            const type = this.value;
            document.querySelectorAll('.transaction-row').forEach(row => {
                if (type === 'all' || row.dataset.type === type) {
                    row.style.display = '';
                } else {
                    row.style.display = 'none';
                }
            });
        });
    });

    // 10. Copy Account Number
    document.querySelectorAll('.copy-account')?.forEach(btn => {
        btn.addEventListener('click', function () {
            const accountNum = this.dataset.account;
            navigator.clipboard.writeText(accountNum).then(() => {
                const original = this.innerHTML;
                this.innerHTML = '✅ Copied!';
                this.style.background = '#10b981';
                setTimeout(() => {
                    this.innerHTML = original;
                    this.style.background = '';
                }, 2000);
            });
        });
    });

    // 11. Number Formatting (Rupee symbol)
    document.querySelectorAll('[data-format="rupee"]').forEach(el => {
        const value = parseFloat(el.textContent);
        el.textContent = `₹${value.toLocaleString('en-IN', { maximumFractionDigits: 2 })}`;
    });

    // 12. Form Validation Enhancement
    document.querySelectorAll('form').forEach(form => {
        form.addEventListener('submit', function () {
            const submitBtn = form.querySelector('button[type="submit"]');
            if (submitBtn) {
                const originalText = submitBtn.innerHTML;
                submitBtn.innerHTML = '<i class="bi bi-hourglass-split me-2"></i>Processing...';
                submitBtn.disabled = true;

                setTimeout(() => {
                    submitBtn.innerHTML = originalText;
                    submitBtn.disabled = false;
                }, 3000);
            }
        });
    });

    // 13. Mobile Menu Toggle Enhancement
    document.querySelector('.navbar-toggler')?.addEventListener('click', function () {
        document.body.classList.toggle('menu-open');
    });

    // 14. Auto-hide Alerts
    document.querySelectorAll('.alert').forEach(alert => {
        setTimeout(() => {
            alert.style.opacity = '0';
            setTimeout(() => alert.remove(), 500);
        }, 5000);
    });

    console.log('✅ All BankingPro features activated!');
});
